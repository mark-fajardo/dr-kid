using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneScript : MonoBehaviour
{
    public Dialog dialog;
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
        StartCoroutine(StartDialog());
    }

    IEnumerator StartDialog ()
    {
        yield return new WaitForSeconds(2f);
        task = "initial";
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    public void DisplayNextDialog ()
    {
        finished = FindObjectOfType<DialogManager>().DisplayNextDialog();
        if (finished == false)
        {
            return;
        }

        if (task == "initial")
        {
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
        answersAnimator.SetBool("AnswerIsOpen", true);
        yield return new WaitForSeconds(5f);
        answersAnimator.SetBool("AnswerIsOpen", false);
        nextStage();
    }

    public void drinkBlood()
    {
        task = "initial";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(drinkBloodDialog);
    }

    public void doNothing()
    {
        task = "doNothing";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(doNothingDialog);
    }

    public void wipeWithDirtyTowel()
    {
        task = "initial";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(dirtyTowelDialog);
    }

    public void cleanTheCut()
    {
        task = "correct";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(cleanTheCutDialog);
    }

    private void cindyDoNothing()
    {
        task = "cindyDoNothing";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        audioSource.PlayOneShot(cindyCry);
        FindObjectOfType<DialogManager>().StartDialog(cindyDoNothingDialog);
    }

    private void finalDoNothing()
    {
        task = "initial";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(finalDoNothingDialog);
    }

    private void nextStage()
    {
        task = "nextDialog";
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
        FindObjectOfType<DialogManager>().StartDialog(nextStageDialog);
    }
}
