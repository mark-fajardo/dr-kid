using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class MainMenu : MonoBehaviour
{
    public GameObject SubscriptionCover;

    public GameObject GenderContainer;
    public GameObject FirstAidContainer;

    void Start()
    {
        var parameterDate = DateTime.ParseExact("05/30/2021", "MM/dd/yyyy", CultureInfo.InvariantCulture);
        var todaysDate = DateTime.Today;

        if (parameterDate <= todaysDate)
        {
            //SubscriptionCover.SetActive(true);
        }
    }

    /**
     * Exit game.
     */
    public void ExitGame ()
    {
        Application.Quit();
    }

    public void ShowFirstAid()
    {
        SetupGenderSetting();
    }

    private void SetupGenderSetting()
    {
        List<string> ShowGenderSetting = FindObjectOfType<DB>().CheckGenderSetting();
        ShowGenderSetting.ForEach(x => ManageGenderSetting(x));
    }

    private void ManageGenderSetting(string Config)
    {
        if (Config == "gen_setting_0")
        {
            GenderContainer.SetActive(true);
            return;
        }

        FirstAidContainer.SetActive(true);
    }

    public void UpdateGenderSetting()
    {
        FindObjectOfType<DB>().UpdateGenderConfigSetting();
    }
}
