using System;
using UnityEngine;
using UnityEngine.UI;

public class OpenImage : MonoBehaviour
{
     private Image _img;
    private void Awake()
    {
        _img = GetComponent<Image>();
        OpenImageMax();
    }
    private void OpenImageMax()
    {
        _img.sprite = DownloadImageFromServer.Gallery.SelectSprite;
    }
}
