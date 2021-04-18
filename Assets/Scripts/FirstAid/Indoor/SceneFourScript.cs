using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFourScript : MonoBehaviour
{
    public GameObject Intro;
    public GameObject IntroTwo;
    public GameObject TutorialOne;
    public GameObject TutorialTwo;
    public GameObject AssessmentOne;

    public Dialog CindyIntroDialog;
    public Dialog NarratorTutorialDialog;

    public Dialog CindyChoosingDialog;
    public Dialog OptionOneDialog;
    public Dialog OptionTwoDialog;
    public Dialog OptionThreeDialog;
    public Dialog OptionFourDialog;

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
        FindObjectOfType<DialogManager>().StartDialog(CindyIntroDialog);
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

    public void OptionOne() // Correct One
    {
        Option = 1;
        FindObjectOfType<DB>().UpdateLevelDone(4, 5);
        ChooseOption(OptionOneDialog);
    }

    public void OptionTwo() // Lotion
    {
        Option = 2;
        ChooseOption(OptionTwoDialog);
    }

    public void OptionThree() // Thermometer
    {
        Option = 3;
        ChooseOption(OptionThreeDialog);
    }

    public void OptionFour() // Gauze
    {
        Option = 4;
        ChooseOption(OptionFourDialog);
    }

    private void ChooseReset()
    {
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
        FindObjectOfType<FirstAidMenu>().ShowChoices();
    }

    private void ChooseOption(Dialog dialog)
    {
        Talking = "NarratorChoosing";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().HideChoices();
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    private void ChoosingQue()
    {
        if (DialogFinished == true)
        {
            if (Option == 1)
            {
                FindObjectOfType<LevelLoader>().LoadLevel(5);
            }
            else
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
            else if (DialogQue == 7)
            {
                AssessmentOne.SetActive(false);
                FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
                FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            }
        }
    }

    private void CindyQue()
    {
        if (DialogQue == 2)
        {
            FindObjectOfType<FirstAidMenu>().MoveCindyRight();
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            Intro.SetActive(true);
        }
        else if (DialogQue == 3)
        {
            Intro.SetActive(false);
            IntroTwo.SetActive(true);
        }
        else if (DialogQue == 4)
        {
            IntroTwo.SetActive(false);
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
        if (DialogQue == 2)
        {
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
            TutorialOne.SetActive(true);
        }
        else if (DialogQue == 4)
        {
            TutorialOne.SetActive(false);
            TutorialTwo.SetActive(true);
        }
        else if (DialogQue == 6)
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
        FindObjectOfType<DialogManager>().StartDialog(NarratorTutorialDialog);
    }

    private void CindyChoose()
    {
        Talking = "CindyChoosing";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(CindyChoosingDialog);
    }
}
