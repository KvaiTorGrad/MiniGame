using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public enum InitButton
    {
        GoToGallery = 0,
        OpenImage = 1,
        CloseImage = 2
    }
    [SerializeField] private InitButton _initButton;
    public InitButton Button { get => _initButton; set => _initButton = value; }
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    void Start()
    {
        if (_initButton == InitButton.GoToGallery)
            _button.onClick.AddListener(delegate { NextLoadScene("Gallery", true); });
        else if (_initButton == InitButton.OpenImage)
            _button.onClick.AddListener(delegate { OpenImageMax("Viewing", false); });
        else
            _button.onClick.AddListener(delegate { NextLoadScene("Gallery", false); });
    }
    private void NextLoadScene(string nameToScene, bool isDownloadFile)
    {
        if (isDownloadFile)
            DownloadImageFromServer.DownloadImg.Invoke();
        Screen.orientation = ScreenOrientation.Portrait;
        new LoadNextScene(nameToScene, this, isDownloadFile);
    }
    private void OpenImageMax(string nameToScene, bool isDownloadFile)
    {
        var spriteSelectImage = GetComponent<Image>().sprite;
        DownloadImageFromServer.Gallery.SelectSprite = spriteSelectImage;
        Screen.orientation = ScreenOrientation.AutoRotation;
        new LoadNextScene(nameToScene, this, isDownloadFile);


    }

}
