using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CalamityScript : MonoBehaviour
{
    public Button LevelSixteen;
    public Button LevelSeventeen;
    public Button LevelEighteen;
    public Button LevelNineteen;
    public Button LevelTwenty;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(16,17,18,19,20)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));
    }

    private void SetupDisabledButtons(string Level)
    {
        switch (Level)
        {
            case "16":
                LevelSixteen.interactable = false;
                break;
            case "17":
                LevelSeventeen.interactable = false;
                break;
            case "18":
                LevelEighteen.interactable = false;
                break;
            case "19":
                LevelNineteen.interactable = false;
                break;
            case "20":
                LevelTwenty.interactable = false;
                break;
        }
    }
}
