using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    private int CurrentSceneIndex;

    public void LoadLevel (int sceneIndex)
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(AddLoading(sceneIndex));
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
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private void OperationOnCompleted(AsyncOperation obj)
    {
        loadingBar.value = 1;
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
