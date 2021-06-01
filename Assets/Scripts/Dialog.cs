using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;

    [TextArea(3, 10)]
    public string[] dialogs;

    [TextArea(3, 10)]
    public string[] tagalogDialogs;

    public AudioClip[] tagalogAudio;

    public AudioClip[] englishDrKidBoyAudio;

    public AudioClip[] tagalogDrKidBoyAudio;
}
