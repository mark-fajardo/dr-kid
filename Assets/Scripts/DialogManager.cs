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
    private string SelectedGender;

    private GameObject DialogBackBtn;
    public Image Narrator;

    private Dialog DialogModel;

    public void StartDialog(Dialog dialog, AudioClip[]? NarrationAudio = null)
    {
        DialogBackBtn = GameObject.Find("Canvas/DialogBox/DialogBackBtn");
        animator.SetBool("DialogIsOpen", true);
        nameText.text = dialog.name;
        DialogModel = dialog;
        SetupLanguage();
        SetupGender();

        if (SelectedLanguage == "lang_0")
        {
            dialogs = dialog.dialogs;
            NarrationAudioGroup = NarrationAudio;
        } 
        else
        {
            dialogs = dialog.tagalogDialogs;
            NarrationAudioGroup = dialog.tagalogAudio;
        }

        SetupDrKidAudio();
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

    private void SetupGender()
    {
        List<string> ShowGender = FindObjectOfType<DB>().CheckGender();
        ShowGender.ForEach(x => ManageGender(x));
    }

    private void ManageGender(string Config)
    {
        SelectedGender = Config;
        if (SelectedGender == "gen_1")
        {
            var DrKidBoy = Resources.Load<Sprite>("Characters/dr-kid-boy");
            //GameObject.Find("Canvas/Characters/Narrator").GetComponent<Image>().sprite = DrKidBoy;
            Narrator.sprite = DrKidBoy;
        }
    }

    private void SetupDrKidAudio()
    {
        if (DialogModel.name != "Dr. Kid")
        {
            return;
        }

        if (SelectedGender == "gen_1")
        {
            if (SelectedLanguage == "lang_0")
            {
                NarrationAudioGroup = DialogModel.englishDrKidBoyAudio;
            } 
            else
            {
                NarrationAudioGroup = DialogModel.tagalogDrKidBoyAudio;
            }
        }
    }
}
