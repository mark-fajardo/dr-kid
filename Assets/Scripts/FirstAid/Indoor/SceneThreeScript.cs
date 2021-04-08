using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThreeScript : MonoBehaviour
{
    public GameObject IntroOne;
    public GameObject TutorialOne;
    public GameObject TutorialTwo;
    public GameObject AssessmentOne;

    public Dialog CindyIntroDialog;
    public Dialog NarratorTutorialDialog;
    public Dialog CindyTutorialDialog;
    public Dialog NarratorTutorialTwoDialog;

    public Dialog OptionOneDialog;
    public Dialog OptionTwoDialog;
    public Dialog OptionThreeDialog;
    public Dialog OptionFourDialog;

    private int DialogQue = 0;
    private int Option = 1;
    private string Talking = "CindyIntro";
    private bool DialogFinished = false;

    // Start is called before the first frame update
    void Start()
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

        else if (Talking == "CindyTutorial")
        {
            CindyTutorialQue();
        }

        else if (Talking == "NarratorTutorialTwo")
        {
            NarratorTutorialTwoQue();
        }

        else if (Talking == "NarratorChoosing")
        {
            ChoosingQue();
        }

    }

    private void CindyQue()
    {
        if (DialogQue == 2)
        {
            FindObjectOfType<FirstAidMenu>().MoveCindyRight();
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            IntroOne.SetActive(true);
        }
        else if (DialogQue == 4)
        {
            IntroOne.SetActive(false);
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveCindyLeft();
        }
        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
            NarratorTutorial();
        }
    }

    private void NarratorTutorial()
    {
        Talking = "NarratorTutorial";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
        FindObjectOfType<DialogManager>().StartDialog(NarratorTutorialDialog);
    }

    private void NarratorTutorialQue()
    {
        if (DialogQue == 1)
        {
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
            TutorialOne.SetActive(true);
        }

        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
            FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
            TutorialOne.SetActive(false);
            CindyTutorial();
        }
    }

    private void CindyTutorial()
    {
        Talking = "CindyTutorial";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(CindyTutorialDialog);
        FindObjectOfType<FirstAidMenu>().MakeCindyCry();
    }

    private void CindyTutorialQue()
    {
        if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetCindyPanel(false);
            NarratorTutorialTwo();
        }
    }

    private void NarratorTutorialTwo()
    {
        Talking = "NarratorTutorialTwo";
        DialogQue = 0;
        FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(NarratorTutorialTwoDialog);
    }

    private void NarratorTutorialTwoQue()
    {
        if (DialogQue == 1)
        {
            FindObjectOfType<FirstAidMenu>().GrowIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorRight();
            TutorialTwo.SetActive(true);
        }

        else if (DialogQue == 3)
        {
            FindObjectOfType<FirstAidMenu>().ShrinkIntro();
            FindObjectOfType<FirstAidMenu>().MoveNarratorLeft();
            TutorialTwo.SetActive(false);
        }

        else if (DialogFinished == true)
        {
            FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
            FindObjectOfType<FirstAidMenu>().ShowChoices();
            FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        }
    }

    public void OptionOne() // Correct One
    {
        Option = 1;
        ChooseOption(OptionOneDialog);
    }

    public void OptionTwo() // Alcohol
    {
        Option = 2;
        ChooseOption(OptionTwoDialog);
    }

    public void OptionThree() // Betadine
    {
        Option = 3;
        ChooseOption(OptionThreeDialog);
    }

    public void OptionFour() // Gauze
    {
        Option = 4;
        ChooseOption(OptionFourDialog);
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

    private void ChooseReset()
    {
        FindObjectOfType<FirstAidMenu>().SetCindyPanel(true);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
        FindObjectOfType<FirstAidMenu>().ShowChoices();
    }

    private void ChoosingQue()
    {
        if (DialogFinished == true)
        {
            if (Option == 1)
            {
                FindObjectOfType<LevelLoader>().LoadLevel(3);
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
}
