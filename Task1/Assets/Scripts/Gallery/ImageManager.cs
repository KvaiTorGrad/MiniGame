using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private Image _shablonImg;
    private Scrollbar _scroll;
    private List<Image> _images = new List<Image>();
    public static Action InitNewImage;
    void Awake()
    {
        GetImageFromGallery();
        InitNewImage += GetImageFromGallery;
        _scroll = GetComponentInChildren<Scrollbar>();
        new InitSizeImg(GetComponentInChildren<GridLayoutGroup>());
    }
    private void GetImageFromGallery()
    {
        if(_images.Count == 0)
            for (int i = 0; i < DownloadImageFromServer.Gallery.Textures.Count; i++)
                CreateShablonOnScene(i);
        else
            CreateShablonOnScene(DownloadImageFromServer.Gallery.Textures.Count - 1);
    }
    public void DownloadNewImage(float valueScroll)
    {
        if (_scroll.value <= 0.3f)
            DownloadImageFromServer.DownloadImg.Invoke();
    }
    private void CreateShablonOnScene(int textureCount)
    {
        var texture = DownloadImageFromServer.Gallery.Textures[textureCount];
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 10);
        var image = Instantiate(_shablonImg, _content);
        image.sprite = sprite;
        image.AddComponent<ButtonManager>().Button = ButtonManager.InitButton.OpenImage;
        _images.Add(image);
        image.name = _images.Count.ToString();

    }
    private void OnDestroy()
    {
        InitNewImage -= GetImageFromGallery;
    }
}
