using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] private Button beginnerButton;
    [SerializeField] private Button intermedietButton;
    [SerializeField] private Button advanceButton;
    [SerializeField] private Button backButton;

    void Start()
    {
        if (beginnerButton != null)
            beginnerButton.onClick.AddListener(() => SceneManager.LoadScene("BeginnerStage"));

        if (intermedietButton != null)
            intermedietButton.onClick.AddListener(() => SceneManager.LoadScene("IntermedietStage"));

        if (advanceButton != null)
            advanceButton.onClick.AddListener(() => SceneManager.LoadScene("AdvanceStage"));

        if (backButton != null)
            backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }

}
