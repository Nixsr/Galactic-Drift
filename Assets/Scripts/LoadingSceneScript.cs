using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadSceneScript : MonoBehaviour
{
    public string sceneToLoad;
    public Text progressText;

    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            if (progressText != null)
            {
                progressText.text = "Loading... " + (progress * 100) + "%";
            }
            yield return null;
        }
    }
}
