using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickFx;

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickFx);
    }
}
