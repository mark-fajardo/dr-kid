using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneScript : MonoBehaviour
{
    public Dialog dialog;
    public Dialog cindyHurtDialog;
    public Dialog nextStageDialog;
    public Dialog drinkBloodDialog;
    public Dialog doNothingDialog;
    public Dialog cindyDoNothingDialog;
    public Dialog finalDoNothingDialog; 
    public Dialog dirtyTowelDialog;
    public Dialog cleanTheCutDialog;

    public Animator choicesLeftAnimator;
    public Animator choicesRightAnimator;

    public Animator answersAnimator;

    public AudioSource audioSource;
    public AudioClip cindyCry;

    private string task;
    private bool finished;

    void Start ()
    {
        cindyHurt();
    }

    IEnumerator StartDialog ()
    {
        yield return new WaitForSeconds(1f);
        task = "initial";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    public void DisplayNextDialog ()
    {
        finished = FindObjectOfType<DialogManager>().DisplayNextDialog();
        if (finished == false)
        {
            return;
        }

        if (task == "cindyHurt")
        {
            StartCoroutine(StartDialog());
        }

        else if (task == "initial")
        {
            FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
            FindObjectOfType<FirstAidMenu>().SetCindySadPanel(true);
            choicesLeftAnimator.SetBool("ChoicesLeftOpen", true);
            choicesRightAnimator.SetBool("ChoicesRightOpen", true);
        } 
        
        else if (task == "doNothing")
        {
            cindyDoNothing();
        } 
        
        else if (task == "cindyDoNothing")
        {
            finalDoNothing();
        }

        else if (task == "correct")
        {
            StartCoroutine(LoadAnswer());
        }

        else if (task == "nextDialog")
        {
            FindObjectOfType<LevelLoader>().LoadLevel(2);
        }
    }

    IEnumerator LoadAnswer()
    {
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
        answersAnimator.SetBool("AnswerIsOpen", true);
        yield return new WaitForSeconds(5f);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        answersAnimator.SetBool("AnswerIsOpen", false);
        nextStage();
    }

    public void drinkBlood()
    {
        task = "initial";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(drinkBloodDialog);
    }

    public void doNothing()
    {
        task = "doNothing";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(doNothingDialog);
    }

    public void wipeWithDirtyTowel()
    {
        task = "initial";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(dirtyTowelDialog);
    }

    public void cleanTheCut()
    {
        task = "correct";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(cleanTheCutDialog);
    }

    private void cindyHurt()
    {
        task = "cindyHurt";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(true);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        audioSource.PlayOneShot(cindyCry);
        FindObjectOfType<DialogManager>().StartDialog(cindyHurtDialog);
    }

    private void cindyDoNothing()
    {
        task = "cindyDoNothing";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(true);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(false);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        audioSource.PlayOneShot(cindyCry);
        FindObjectOfType<DialogManager>().StartDialog(cindyDoNothingDialog);
    }

    private void finalDoNothing()
    {
        task = "initial";
        FindObjectOfType<FirstAidMenu>().SetCindySadPanel(false);
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(finalDoNothingDialog);
    }

    private void nextStage()
    {
        task = "nextDialog";
        FindObjectOfType<FirstAidMenu>().SetNarratorPanel(true);
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(nextStageDialog);
    }
}
