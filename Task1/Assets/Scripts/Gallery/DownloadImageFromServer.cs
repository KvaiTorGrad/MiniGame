using System;
using UnityEngine;

public class DownloadImageFromServer : MonoBehaviour
{
    [SerializeField] private static Gallery _gallery;
    private static float _progressValue;
    private static bool _isStartCoroutine;
    private int _maxImgCountOnServer;
    private int _allDownloadImg;
    [SerializeField] private int countLoadImg;

    public static Gallery Gallery { get => _gallery; }
    public static float ProgressValue { get => _progressValue; set => _progressValue = value; }
    public static bool IsStartCoroutine { get => _isStartCoroutine; set => _isStartCoroutine = value; }

    public static Action DownloadImg;
    public static DownloadImageFromServer gameManager = null;

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }

        WebRequests.LoadProgress += Progress;
        _gallery = Resources.Load("Gallery") as Gallery;
        DownloadImg += DownloadNewTexture;
    }
    private void Start()
    {
        _maxImgCountOnServer = 66;
        countLoadImg = 6;
    }
    private void DownloadNewTexture()
    {
        if (!_isStartCoroutine)
        {
            _isStartCoroutine = true;
            for (int i = 0; i < countLoadImg; i++)
            {
                if (_allDownloadImg == _maxImgCountOnServer) break;
                _allDownloadImg++;
                new WebRequests(
                (string error) =>
                    {
                        Debug.LogError("Error " + error);
                    },
                (Texture2D texture) =>
                    {
                        _gallery.Textures.Add(texture);
                    },
                _allDownloadImg,
                this,
                countLoadImg);
                PlayerPrefs.SetInt("CountAllImg", _allDownloadImg);
            }
        }
    }

    private void Progress()
    {
        _progressValue += 100 / countLoadImg;
    }
    private void OnDestroy()
    {
        WebRequests.LoadProgress -= Progress;
        DownloadImg -= DownloadNewTexture;
    }
    private void OnApplicationQuit()
    {
        Gallery.Textures.Clear();
        PlayerPrefs.DeleteAll();
    }
}
