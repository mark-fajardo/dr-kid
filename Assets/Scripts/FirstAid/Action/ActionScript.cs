using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionScript : MonoBehaviour
{
    public Button LvlEleven;
    public Button LvlTwelve;
    public Button LvlThirteen;
    public Button LvlFourteen;
    public Button LvlFifteen;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(11,12,13,14,15)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));
    }

    private void SetupDisabledButtons(string Level)
    {
        switch (Level)
        {
            case "11":
                LvlEleven.interactable = false;
                break;
            case "12":
                LvlTwelve.interactable = false;
                break;
            case "13":
                LvlThirteen.interactable = false;
                break;
            case "14":
                LvlFourteen.interactable = false;
                break;
            case "15":
                LvlFifteen.interactable = false;
                break;
        }
    }
}
