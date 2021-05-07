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
        audioSource.Stop();
        audioSource.PlayOneShot(clickFx);
    }

    public void WrongChoiceSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(wrongChoice);
    }

    public void CorrectChoiceSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(correctChoice);
    }
}
