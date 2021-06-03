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
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(6,7,8,9,10)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));

        List<int[]> ShowScores = FindObjectOfType<DB>().CheckScore("(6,7,8,9,10)");
        ShowScores.ForEach(x => SetupStars(x));
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

    private void SetupStars(int[] Level)
    {
        var Star3 = Resources.Load<Sprite>("star/star_3");
        var Star2 = Resources.Load<Sprite>("star/star_2");
        var Star1 = Resources.Load<Sprite>("star/star_1");

        switch (Level[0])
        {
            case 6:
                ImageLoop = LvlOneStar;
                break;
            case 7:
                ImageLoop = LvlTwoStar;
                break;
            case 8:
                ImageLoop = LvlThreeStar;
                break;
            case 9:
                ImageLoop = LvlFourStar;
                break;
            case 10:
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
