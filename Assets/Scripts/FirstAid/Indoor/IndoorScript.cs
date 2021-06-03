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
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(1,2,3,4,5)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));

        List<int[]> ShowScores = FindObjectOfType<DB>().CheckScore("(1,2,3,4,5)");
        ShowScores.ForEach(x => SetupStars(x));
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

    private void SetupStars(int[] Level)
    {
        var Star3 = Resources.Load<Sprite>("star/star_3");
        var Star2= Resources.Load<Sprite>("star/star_2");
        var Star1= Resources.Load<Sprite>("star/star_1");

        switch (Level[0])
        {
            case 1:
                ImageLoop = LvlOneStar;
                break;
            case 2:
                ImageLoop = LvlTwoStar;
                break;
            case 3:
                ImageLoop = LvlThreeStar;
                break;
            case 4:
                ImageLoop = LvlFourStar;
                break;
            case 5:
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
