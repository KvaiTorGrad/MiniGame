using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene
{
    private ParametrsForLoadNextScene _parametrsForLoadNextScene;
    public LoadNextScene(string nameLoadScene, MonoBehaviour monoBehaviour, bool isDownloadFile)
    {
        _parametrsForLoadNextScene = Resources.Load("Parametrs For Load Next Scene") as ParametrsForLoadNextScene;
        if (isDownloadFile)
            monoBehaviour.StartCoroutine(LoadScene(nameLoadScene));
        else
            monoBehaviour.StartCoroutine(LoadSceneTest(nameLoadScene));
    }
    private IEnumerator LoadScene(string nameLoadScene)
    {
        CreateLoadPanel();
        var asyncOperation = SceneManager.LoadSceneAsync(nameLoadScene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.allowSceneActivation)
        {
            _parametrsForLoadNextScene.ProgressBarText.text = $"Загрузка...{DownloadImageFromServer.ProgressValue:0}%";
            _parametrsForLoadNextScene.ProgressBarImage.fillAmount = DownloadImageFromServer.ProgressValue / 100;
            asyncOperation.allowSceneActivation = _parametrsForLoadNextScene.IsLoadScene;
            yield return null;
        }
        DownloadImageFromServer.ProgressValue = 0f;
    }
    private IEnumerator LoadSceneTest(string nameLoadScene)
    {
        CreateLoadPanel();
        var asyncOperation = SceneManager.LoadSceneAsync(nameLoadScene);
        float progress;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _parametrsForLoadNextScene.ProgressBarText.text = $"Загрузка...{(progress * 100):0}%";
            _parametrsForLoadNextScene.ProgressBarImage.fillAmount = progress;
            yield return null;
        }
    }
    private void CreateLoadPanel()
    {
        UnityEngine.Object.Instantiate(_parametrsForLoadNextScene.LoadPanel);
        _parametrsForLoadNextScene.ProgressBarText = GameObject.FindGameObjectWithTag("LoadText").GetComponent<TMP_Text>();
        _parametrsForLoadNextScene.ProgressBarImage = GameObject.FindGameObjectWithTag("LoadBar").GetComponent<Image>();
    }
}
