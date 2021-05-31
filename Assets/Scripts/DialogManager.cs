using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;

    public Animator animator;

    public AudioSource NarrationSource;
    private AudioClip[] NarrationAudioGroup;
    private bool HasAudioGlobal;

    private Queue<string> dialogs;

    private int TotalDialog;

    private string SelectedLanguage;

    public void StartDialog(Dialog dialog, AudioClip[]? NarrationAudio = null)
    {
        NarrationAudioGroup = NarrationAudio;
        animator.SetBool("DialogIsOpen", true);
        dialogs = new Queue<string>();
        dialogs.Clear();
        nameText.text = dialog.name;
        SetupLanguage();

        if (SelectedLanguage == "lang_0")
        {
            foreach (string sDialog in dialog.dialogs)
            {
                dialogs.Enqueue(sDialog);
            }
        } 
        else
        {
            foreach (string sDialog in dialog.tagalogDialogs)
            {
                dialogs.Enqueue(sDialog);
            }
        }

        TotalDialog = dialogs.Count;
        DisplayNextDialog();
    }

    public bool DisplayNextDialog()
    {
        if (dialogs.Count == 0)
        {
            EndDialog();
            return true;
        }
        try
        {
            if (NarrationAudioGroup != null)
            {
                NarrationSource.Stop();
                NarrationSource.PlayOneShot(NarrationAudioGroup[TotalDialog - dialogs.Count]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error");
            Console.WriteLine(e.Message);
        }

        string dialog = dialogs.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeDialog(dialog));
        return false;
    }

    IEnumerator TypeDialog (string dialog)
    {
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        animator.SetBool("DialogIsOpen", false);
    }

    private void SetupLanguage()
    {
        List<string> ShowLanguage = FindObjectOfType<DB>().CheckLanguage();
        ShowLanguage.ForEach(x => ManageLanguage(x));
    }

    private void ManageLanguage(string Config)
    {
        SelectedLanguage = Config;
    }
}
