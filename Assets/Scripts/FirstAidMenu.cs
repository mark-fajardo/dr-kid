using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstAidMenu : MonoBehaviour
{
    // Cindy
    public Animator CindyAnimator;
    public GameObject CindyContainer;

    // Narrator
    public Animator NarratorAnimator;
    public GameObject NarratorContainer;

    public Animator IntroAnimator;
    public GameObject IntroContainer;

    public AudioSource AudioSource;
    public AudioClip CindyCry;

    public Animator choicesLeftAnimator;
    public Animator choicesRightAnimator;

    public void ShowChoices()
    {
        ShowLeftChoices();
        ShowRightChoices();
    }

    public void HideChoices()
    {
        HideLeftChoices();
        HideRightChoices();
    }

    public void ShowLeftChoices()
    {
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", true);
    }

    public void ShowRightChoices()
    {
        choicesRightAnimator.SetBool("ChoicesRightOpen", true);
    }

    public void HideLeftChoices()
    {
        choicesLeftAnimator.SetBool("ChoicesLeftOpen", false);
    }

    public void HideRightChoices()
    {
        choicesRightAnimator.SetBool("ChoicesRightOpen", false);
    }

    public void MakeCindyCry()
    {
        AudioSource.PlayOneShot(CindyCry);
    }

    public void SetNarratorPanel(bool active)
    {
        NarratorContainer.SetActive(active);
    }

    public void SetCindyPanel(bool active)
    {
        CindyContainer.SetActive(active);
    }

    public void BackToFirstAidLevels()
    {
        SceneManager.LoadScene(0);
    }

    public void GrowIntro()
    {
        ShowIntro();
    }

    public void ShrinkIntro()
    {
        ShowIntro(false);
    }

    public void MoveCindyRight()
    {
        MoveCindy();
    }

    public void MoveCindyLeft()
    {
        MoveCindy(false);
    }

    public void MoveNarratorRight()
    {
        MoveNarrator();
    }

    public void MoveNarratorLeft()
    {
        MoveNarrator(false);
    }

    /**
     * FirstAid Level 1
     */
    public void LevelOne()
    {
        SceneManager.LoadScene(1);
    }

    private void MoveCindy(bool move = true)
    {
        CindyAnimator.SetBool("CindyIsMoved", move);
    }

    private void MoveNarrator(bool move = true)
    {
        NarratorAnimator.SetBool("NarratorHasMoved", move);
    }

    private void ShowIntro(bool show = true)
    {
        if (show == true)
        {
            IntroContainer.SetActive(true);
            IntroAnimator.SetBool("ShowIntro", true);
        }
        else
        {
            StartCoroutine(LoadIntro());
        }
    }

    IEnumerator LoadIntro()
    {
        IntroAnimator.SetBool("ShowIntro", false);
        yield return new WaitForSeconds(1f);
        IntroContainer.SetActive(false);
    }
}