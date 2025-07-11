using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultController : MonoBehaviour
{
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button repeatButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        if (GameManager.isWinning)
        {
            winText.SetActive(true);
            loseText.SetActive(false);
        }
        else if (GameManager.isLose)
        {
            winText.SetActive(false);
            loseText.SetActive(true);
        }
    }

    private void OnEnable()
    {
        backButton.onClick.AddListener(() => SceneManager.LoadScene("BeginnerStage"));
        repeatButton.onClick.AddListener(() => SceneManager.LoadScene("GamePlay"));
        homeButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        scoreText.text = "Score: " + GameManager.score.ToString();
        GameManager.ResetGame();
    }
}
