using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequests
{
    public static Action LoadProgress;
    public static int DownloadedImages { get; set; }
    public WebRequests(Action<string> onError, Action<Texture2D> onSuccess, int AmountImg, MonoBehaviour monoBehaviour,int countLoadImg)
    {
        monoBehaviour.StartCoroutine(WebTexture(onError, onSuccess, AmountImg, countLoadImg));
    }

    #region GetTexture
    private IEnumerator WebTexture(Action<string> onError, Action<Texture2D> onSuccess, int AmountImg, int countLoadImg)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://data.ikppbb.com/test-task-unity-data/pics/"))
        {

            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
                onError("Ошибка: " + unityWebRequest.error);
            else
            {
                using (UnityWebRequest unityWebRequestTexture = UnityWebRequestTexture.GetTexture($"http://data.ikppbb.com/test-task-unity-data/pics/{AmountImg}.jpg"))
                {
                    LoadProgress.Invoke();
                    yield return unityWebRequestTexture.SendWebRequest();
                    if (unityWebRequestTexture.result == UnityWebRequest.Result.ConnectionError || unityWebRequestTexture.result == UnityWebRequest.Result.ProtocolError)
                        onError(unityWebRequestTexture.error);
                    else
                    {
                        DownloadHandlerTexture downloadHandlerTexture = unityWebRequestTexture.downloadHandler as DownloadHandlerTexture;
                        onSuccess(downloadHandlerTexture.texture);
                        ImageManager.InitNewImage?.Invoke();
                    }
                }
            }
            DownloadedImages++;
            if (DownloadedImages == countLoadImg)
            {
                DownloadImageFromServer.IsStartCoroutine = false;
                DownloadedImages = 0;
            }
            ParametrsForLoadNextScene parametrsForLoadNextScene = Resources.Load("Parametrs For Load Next Scene") as ParametrsForLoadNextScene;
            parametrsForLoadNextScene.IsLoadScene = true;
        }
    }
}
#endregion

