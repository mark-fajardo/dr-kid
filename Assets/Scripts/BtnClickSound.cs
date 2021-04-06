using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickSound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clickFx;
    public AudioClip wrongChoice;
    public AudioClip correctChoice;

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickFx);
    }

    public void WrongChoiceSound()
    {
        audioSource.PlayOneShot(wrongChoice);
    }

    public void CorrectChoiceSound()
    {
        audioSource.PlayOneShot(correctChoice);
    }
}
