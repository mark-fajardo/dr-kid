using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNineScript : MonoBehaviour
{
    public GameObject Intro;
    public GameObject TutorialOne;
    public GameObject AssessmentOne;

    public Dialog CindyIntroDialog;
    public Dialog NarratorTutorialDialog;

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

        else if (Talking == "NarratorChoosing")
        {
            ChoosingQue();
        }
    }

    public void OptionOne() // Scissor
    {
        Option = 1;
        FindObjectOfType<DB>().UpdateLevelDone(9, 10);
        ChooseOption(OptionOneDialog);
    }

    public void OptionTwo() // Glue and Gauze
    {
        Option = 2;
        ChooseOption(OptionTwoDialog);
    }

    public void OptionThree() // Correct
    {
        Option = 3;
        ChooseOption(OptionThreeDialog);
    }

    public void OptionFour() // Tape
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
                FindObjectOfType<LevelLoader>().LoadLevel(10);
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
            else if (DialogQue == 4)
            {
                AssessmentOne.SetActive(false);
                FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
                FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            }
        }
    }

    private void CindyQue()
    {
        if (DialogQue == 3)
        {
            FindObjectOfType<FirstAidMenu>().MoveCindyRight();
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            Intro.SetActive(true);
        }
        else if (DialogQue == 4)
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
        if (DialogQue == 2)
        {
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
            TutorialOne.SetActive(true);
        }
        else if (DialogQue == 5)
        {
            TutorialOne.SetActive(false);
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
        FindObjectOfType<DialogManager>().StartDialog(NarratorTutorialDialog);
    }
}
