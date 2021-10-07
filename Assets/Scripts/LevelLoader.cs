using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public Image Star1;
    public Image Star2;
    public Image Star3;
    public GameObject ScoreBoard;

    private int CurrentSceneIndex;

    private int NextSceneIndex;

    public void LoadLevel (int sceneIndex)
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(AddLoading(sceneIndex));
    }

    public void ContinueToNextLevel()
    {
        ScoreBoard.SetActive(false);
        StartCoroutine(AddLoading(NextSceneIndex));
    }

    public void LoadNextLevel(int sceneIndex, int TimesSelected = -1)
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var StarWin = Resources.Load<Sprite>("star/star_win");
        NextSceneIndex = sceneIndex;

        if (TimesSelected <= -1)
        {
            StartCoroutine(AddLoading(sceneIndex));
        }
        else if (TimesSelected == 0)
        {
            ScoreBoard.SetActive(true);
            Star1.sprite = StarWin;
            Star2.sprite = StarWin;
            Star3.sprite = StarWin;
        }
        else if (TimesSelected >= 1 && TimesSelected <= 2)
        {
            ScoreBoard.SetActive(true);
            Star1.sprite = StarWin;
            Star2.sprite = StarWin;
        }
        else if (TimesSelected >= 3)
        {
            ScoreBoard.SetActive(true);
            Star1.sprite = StarWin;
        }
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.completed += OperationOnCompleted;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;

            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator AddLoading(int sceneIndex)
    {
        try
        {
            FindObjectOfType<DialogManager>().StopAudio();
        }
        catch (Exception e)
        {
            Console.WriteLine("Cannot stop narration source.");
            Console.WriteLine(e.Message);
        }
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private void OperationOnCompleted(AsyncOperation obj)
    {
        loadingBar.value = 1;
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
