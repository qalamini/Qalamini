using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button gameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject settingsUI;

    void Start()
    {
        settingsUI.SetActive(false);
        gameButton.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));
        profileButton.onClick.AddListener(() => SceneManager.LoadScene("Profile"));
        settingsButton.onClick.AddListener(() => settingsUI.SetActive(true));
        closeButton.onClick.AddListener(() => settingsUI.SetActive(false));
    }

}
