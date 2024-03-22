using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public SceneName NextScene;

    public Slider LoadingSliderUI;

    void Start()
    {
 /*       if (GameManager.Instance.state == State.Ready)
        {
            NextScene = SceneName.Main;
        }
        else if (GameManager.Instance.state == State.BossDie)
        {
            NextScene = SceneName.Ending;
        }*/
        StartCoroutine(LoadNextScene_Coroutine());
    }
    private IEnumerator LoadNextScene_Coroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync((int)NextScene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            LoadingSliderUI.value = ao.progress;
            if (ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}