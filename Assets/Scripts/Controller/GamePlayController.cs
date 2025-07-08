using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Image[] live;

    void Start()
    {
        if (backButton != null)
            backButton.onClick.AddListener(() => SceneManager.LoadScene("BeginnerStage"));
    }

    void Update()
    {
        // Hitung mundur timer
        if (GameManager.countdownTime > 0 && GameManager.life > 0)
        {
            GameManager.countdownTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(GameManager.countdownTime / 60);
            int seconds = Mathf.FloorToInt(GameManager.countdownTime % 60);

            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            score.text = GameManager.score.ToString();

            for (int i = 0; i < live.Length; i++)
            {
                if (i < GameManager.life) live[i].enabled = true;
                else live[i].enabled = false;
            }
        }
        else
        {
            // Tampilkan stage menang atau kalah
            GameManager.ResetGame();
            SceneManager.LoadScene("BeginnerStage");
            
        }
    }
}
