using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstAidMenu : MonoBehaviour
{
    public GameObject narratorPanel;
    public GameObject cindySadPanel;

    public void SetNarratorPanel(bool active)
    {
        narratorPanel.SetActive(active);
    }

    public void SetCindySadPanel(bool active)
    {
        cindySadPanel.SetActive(active);
    }

    public void BackToFirstAidLevels ()
    {
        SceneManager.LoadScene(0);
    }

    /**
     * FirstAid Level 1
     */
    public void LevelOne ()
    {
        SceneManager.LoadScene(1);
    }
}
