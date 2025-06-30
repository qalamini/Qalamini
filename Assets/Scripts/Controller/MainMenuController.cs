using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button gameButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button profileButton;

    void Start()
    {
        gameButton.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));
        profileButton.onClick.AddListener(() => SceneManager.LoadScene("Profile"));
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game Called"); // Tambahkan debug log untuk memastikan di Editor
    }
}
