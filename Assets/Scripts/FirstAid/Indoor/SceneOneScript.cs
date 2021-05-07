using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneScript : MonoBehaviour
{
    public GameObject Intro;
    public GameObject WashingHands;
    public GameObject TutorialOne;
    public GameObject TutorialTwo;
    public GameObject AssessmentOne;
    public GameObject AssessmentTwo;

    public Dialog CindyIntroDialog;
    public Dialog NarratorTutorialDialog;

    public Dialog CindyChoosingDialog;
    public Dialog OptionOneDialog;
    public Dialog OptionTwoDialog;
    public Dialog OptionThreeDialog;
    public Dialog OptionFourDialog;

    public AudioClip[] CindyIntroDialogAudio;
    public AudioClip[] NarratorTutorialDialogAudio;

    public AudioClip[] CindyChoosingDialogAudio;
    public AudioClip[] OptionOneDialogAudio;
    public AudioClip[] OptionTwoDialogAudio;
    public AudioClip[] OptionThreeDialogAudio;
    public AudioClip[] OptionFourDialogAudio;

    private int DialogQue = 0;
    private int Option = 1;
    private string Talking = "CindyIntro";
    private bool DialogFinished = false;

    public void Start()
    {
        CindyIntro();
    }

    public void CindyIntro()
    {
        FindObjectOfType<FirstAidMenu>().MakeCindyCry();
        FindObjectOfType<DialogManager>().StartDialog(CindyIntroDialog, CindyIntroDialogAudio);
    }

    public void DisplayNextDialog()
    {
        DialogQue += 1;
        DialogFinished = FindObjectOfType<DialogManager>().DisplayNextDialog();
        if (Talking == "CindyIntro")
        {
            CindyQue();
        }

        else if (Talking == "NarratorTutorial")
        {
            NarratorTutorialQue();
        }

        else if (Talking == "CindyChoosing")
        {
            CindyChooseQue();
        }

        else if (Talking == "NarratorChoosing")
        {
            ChoosingQue();
        }
    }

    public void OptionOne()
    {
        Option = 1;
        FindObjectOfType<DB>().UpdateLevelDone(1, 2);
        ChooseOption(OptionOneDialog, OptionOneDialogAudio);
    }

    public void OptionTwo()
    {
        Option = 2;
        ChooseOption(OptionTwoDialog, OptionTwoDialogAudio);
    }

    public void OptionThree()
    {
        Option = 3;
        ChooseOption(OptionThreeDialog, OptionThreeDialogAudio);
    }

    public void OptionFour()
    {
        Option = 4;
        ChooseOption(OptionFourDialog, OptionFourDialogAudio);
    }

    private void ChooseReset()
    {
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
        FindObjectOfType<FirstAidMenu>().ShowChoices();
    }

    private void ChooseOption(Dialog dialog, AudioClip[] audioClips)
    {
        Talking = "NarratorChoosing";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().HideChoices();
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(dialog, audioClips);
    }

    private void ChoosingQue()
    {
        if (DialogFinished == true)
        {
            if (Option == 1)
            {
                FindObjectOfType<LevelLoader>().LoadLevel(2);
            } else
            {
                FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
                ChooseReset();
            }
        }
        else if (Option == 1)
        {
            if (DialogQue == 1)
            {
                FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
                FindObjectOfType<FirstAidMenu>().GrowIntro();
                AssessmentOne.SetActive(true);
            }
            else if (DialogQue == 3)
            {
                AssessmentOne.SetActive(false);
                AssessmentTwo.SetActive(true);
            }
            else if (DialogQue == 5)
            {
                AssessmentTwo.SetActive(false);
                FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
                FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            }
        }
    }

    private void CindyQue()
    {
        if (DialogQue == 1)
        {
            FindObjectOfType<FirstAidMenu>().MoveCindyRight();
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            Intro.SetActive(true);
        }
        else if (DialogQue == 3)
        {
            Intro.SetActive(false);
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveCindyLeft();
        }
        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
            NarratorTutorial();
        }
    }

    private void NarratorTutorialQue()
    {
        if (DialogQue == 4)
        {
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
            WashingHands.SetActive(true);
        }
        else if (DialogQue == 6)
        {
            WashingHands.SetActive(false);
            TutorialOne.SetActive(true);
        }
        else if (DialogQue == 7)
        {
            TutorialOne.SetActive(false);
            TutorialTwo.SetActive(true);
        }
        else if (DialogQue == 8)
        {
            TutorialTwo.SetActive(false);
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
        }
        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
            CindyChoose();
        }
    }

    private void CindyChooseQue()
    {
        if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().ShowChoices();
        }
    }

    private void NarratorTutorial()
    {
        Talking = "NarratorTutorial";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(NarratorTutorialDialog, NarratorTutorialDialogAudio);
    }

    private void CindyChoose()
    {
        Talking = "CindyChoosing";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(CindyChoosingDialog, CindyChoosingDialogAudio);
    }
}
