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

    public Image LvlOneStar;
    public Image LvlTwoStar;
    public Image LvlThreeStar;
    public Image LvlFourStar;
    public Image LvlFiveStar;

    private Image ImageLoop;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(11,12,13,14,15)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));

        List<int[]> ShowScores = FindObjectOfType<DB>().CheckScore("(11,12,13,14,15)");
        ShowScores.ForEach(x => SetupStars(x));
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

    private void SetupStars(int[] Level)
    {
        var Star3 = Resources.Load<Sprite>("star/star_3");
        var Star2 = Resources.Load<Sprite>("star/star_2");
        var Star1 = Resources.Load<Sprite>("star/star_1");

        switch (Level[0])
        {
            case 11:
                ImageLoop = LvlOneStar;
                break;
            case 12:
                ImageLoop = LvlTwoStar;
                break;
            case 13:
                ImageLoop = LvlThreeStar;
                break;
            case 14:
                ImageLoop = LvlFourStar;
                break;
            case 15:
                ImageLoop = LvlFiveStar;
                break;
        }

        if (Level[1] == 3)
        {
            ImageLoop.sprite = Star3;
        }
        else if (Level[1] == 2)
        {
            ImageLoop.sprite = Star2;
        }
        else if (Level[1] == 1)
        {
            ImageLoop.sprite = Star1;
        }
    }
}
