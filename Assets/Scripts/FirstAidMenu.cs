using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstAidMenu : MonoBehaviour
{
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
