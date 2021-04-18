using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class MainMenu : MonoBehaviour
{
    public GameObject SubscriptionCover;

    void Start()
    {
        var parameterDate = DateTime.ParseExact("05/15/2021", "MM/dd/yyyy", CultureInfo.InvariantCulture);
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
}
