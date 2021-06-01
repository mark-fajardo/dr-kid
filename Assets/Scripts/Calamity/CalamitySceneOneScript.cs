using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalamitySceneOneScript : MonoBehaviour
{
    public GameObject Intro;
    public GameObject IntroTwo;
    public GameObject TutorialOne;
    public GameObject TutorialTwo;
    public GameObject TutorialThree;
    public GameObject TutorialFour;
    public GameObject TutorialFive;

    public Dialog CindyIntroDialog;
    public Dialog NarratorTutorialDialog;

    public Dialog OptionOneDialog;
    public Dialog OptionTwoDialog;
    public Dialog OptionThreeDialog;
    public Dialog OptionFourDialog;

    public AudioClip[] CindyIntroDialogAudio;
    public AudioClip[] NarratorTutorialDialogAudio;

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
        Invoke("CindyIntro", 2);
    }

    public void CindyIntro()
    {
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

        else if (Talking == "NarratorChoosing")
        {
            ChoosingQue();
        }
    }

    public void OptionOne()
    {
        Option = 1;
        ChooseOption(OptionOneDialog, OptionOneDialogAudio);
    }

    public void OptionTwo()
    {
        Option = 2;
        FindObjectOfType<DB>().UpdateLevelDone(16, 17);
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
            if (Option == 2)
            {
                FindObjectOfType<LevelLoader>().LoadLevel(12);
            }
            else
            {
                FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
                ChooseReset();
            }
        }
        else if (Option == 2)
        {
            if (DialogQue == 2)
            {
                FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
                FindObjectOfType<FirstAidMenu>().GrowIntro();
                TutorialOne.SetActive(true);
            }
            else if (DialogQue == 3)
            {
                TutorialOne.SetActive(false);
                TutorialTwo.SetActive(true);
            }
            else if (DialogQue == 4)
            {
                TutorialTwo.SetActive(false);
                TutorialThree.SetActive(true);
            }
            else if (DialogQue == 5)
            {
                TutorialThree.SetActive(false);
                TutorialFive.SetActive(true);
            }
            else if (DialogQue == 6)
            {
                TutorialFive.SetActive(false);
                FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
                FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            }
        }
    }

    private void CindyQue()
    {
        if (DialogQue == 2)
        {
            FindObjectOfType<FirstAidMenu>().MoveCindyUmbrellaRight();
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            Intro.SetActive(true);
        }
        else if (DialogQue == 4)
        {
            Intro.SetActive(false);
            IntroTwo.SetActive(true);
        }
        else if (DialogQue == 7)
        {
            IntroTwo.SetActive(false);
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveCindyUmbrellaLeft();
        }
        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
            NarratorTutorial();
        }
    }

    private void NarratorTutorialQue()
    {
        if (DialogQue == 6)
        {
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
            TutorialOne.SetActive(true);
        }
        else if (DialogQue == 7)
        {
            TutorialOne.SetActive(false);
            TutorialTwo.SetActive(true);
        }
        else if (DialogQue == 9)
        {
            TutorialTwo.SetActive(false);
            TutorialThree.SetActive(true);
        }
        else if (DialogQue == 12)
        {
            TutorialThree.SetActive(false);
            TutorialFive.SetActive(true);
        }
        else if (DialogQue == 14)
        {
            TutorialFive.SetActive(false);
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
        }
        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
            FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
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
}
