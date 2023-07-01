using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene : MonoBehaviour
{
    private Button _button;
    [SerializeField] private int _idScene;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { NextScene(_idScene); });
    }
    private void NextScene(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }
}
