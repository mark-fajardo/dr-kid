using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(AddLoading(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

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
}
