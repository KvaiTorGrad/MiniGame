using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ParametrsForLoadNextScene : ScriptableObject
{
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private TMP_Text _progressBarText;
    [SerializeField] private Image _progressBarImage;
    [SerializeField] private bool _isLoadScene;

    public GameObject LoadPanel { get => _loadPanel;}
    public TMP_Text ProgressBarText { get => _progressBarText; set => _progressBarText = value; }
    public Image ProgressBarImage { get => _progressBarImage; set => _progressBarImage = value; }
    public bool IsLoadScene { get => _isLoadScene; set => _isLoadScene = value; }
}
