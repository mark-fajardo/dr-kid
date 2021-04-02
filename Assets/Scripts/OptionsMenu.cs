using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float volume)
    {
        if (volume <= -30)
        {
            volume = -80;
        }

        audioMixer.SetFloat("MainMixer", volume);
    }
}
