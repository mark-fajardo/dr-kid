using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OutdoorScript : MonoBehaviour
{
    public Button LvlSix;
    public Button LvlSeven;
    public Button LvlEight;
    public Button LvlNine;
    public Button LvlTen;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(6,7,8,9,10)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));
    }

    private void SetupDisabledButtons(string Level)
    {
        switch (Level)
        {
            case "6":
                LvlSix.interactable = false;
                break;
            case "7":
                LvlSeven.interactable = false;
                break;
            case "8":
                LvlEight.interactable = false;
                break;
            case "9":
                LvlNine.interactable = false;
                break;
            case "10":
                LvlTen.interactable = false;
                break;
        }
    }
}
