using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IndoorScript : MonoBehaviour
{
    public Button LvlOne;
    public Button LvlTwo;
    public Button LvlThree;
    public Button LvlFour;
    public Button LvlFive;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(1,2,3,4,5)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));
    }

    private void SetupDisabledButtons(string Level)
    {
        switch (Level)
        {
            case "1":
                LvlOne.interactable = false;
                break;
            case "2":
                LvlTwo.interactable = false;
                break;
            case "3":
                LvlThree.interactable = false;
                break;
            case "4":
                LvlFour.interactable = false;
                break;
            case "5":
                LvlFive.interactable = false;
                break;
        }
    }
}
