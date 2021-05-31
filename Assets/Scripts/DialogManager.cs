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

    private string[] dialogs;

    private int TotalDialog = 0;
    private int DialogDisplayIndex = 0;

    private string SelectedLanguage;

    private GameObject DialogBackBtn;

    public void StartDialog(Dialog dialog, AudioClip[]? NarrationAudio = null)
    {
        DialogBackBtn = GameObject.Find("Canvas/DialogBox/DialogBackBtn");
        NarrationAudioGroup = NarrationAudio;
        animator.SetBool("DialogIsOpen", true);
        nameText.text = dialog.name;
        SetupLanguage();

        if (SelectedLanguage == "lang_0")
        {
            dialogs = dialog.dialogs;
        } 
        else
        {
            dialogs = dialog.tagalogDialogs;
        }

        TotalDialog = dialogs.Length;
        DisplayNextDialog();
    }

    public bool DisplayNextDialog()
    {
        if (DialogDisplayIndex == 0)
        {
            DialogBackBtn.SetActive(false);
        }
        else
        {
            DialogBackBtn.SetActive(true);
        }

        if (TotalDialog == DialogDisplayIndex)
        {
            EndDialog();
            DialogDisplayIndex = 0;
            DialogBackBtn.SetActive(false);
            return true;
        }

        try
        {
            if (NarrationAudioGroup != null)
            {
                NarrationSource.Stop();
                NarrationSource.PlayOneShot(NarrationAudioGroup[DialogDisplayIndex]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error");
            Console.WriteLine(e.Message);
        }

        string dialog = dialogs[DialogDisplayIndex];
        StopAllCoroutines();
        StartCoroutine(TypeDialog(dialog));
        DialogDisplayIndex++;
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

    public void ReturnDialog()
    {
        DialogDisplayIndex -= 2;
        DisplayNextDialog();
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
