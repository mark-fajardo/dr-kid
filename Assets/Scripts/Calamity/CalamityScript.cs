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
        List<string> ShowLevels = FindObjectOfType<DB>().CheckLevel("(16,17,18,19,20)");
        ShowLevels.ForEach(x => SetupDisabledButtons(x));

        List<int[]> ShowScores = FindObjectOfType<DB>().CheckScore("(16,17,18,19,20)");
        ShowScores.ForEach(x => SetupStars(x));
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

    private void SetupStars(int[] Level)
    {
        var Star3 = Resources.Load<Sprite>("star/star_3");
        var Star2 = Resources.Load<Sprite>("star/star_2");
        var Star1 = Resources.Load<Sprite>("star/star_1");

        switch (Level[0])
        {
            case 16:
                ImageLoop = LvlOneStar;
                break;
            case 17:
                ImageLoop = LvlTwoStar;
                break;
            case 18:
                ImageLoop = LvlThreeStar;
                break;
            case 19:
                ImageLoop = LvlFourStar;
                break;
            case 20:
                ImageLoop = LvlFiveStar;
                break;
        }

        Debug.Log(Star3);

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
