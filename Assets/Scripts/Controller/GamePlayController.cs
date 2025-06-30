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
    private float countdownTime = 60f; // 60 detik

    void Start()
    {
        if (backButton != null)
            backButton.onClick.AddListener(() => SceneManager.LoadScene("BeginnerStage"));
    }

    void Update()
    {
        // Hitung mundur timer
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(countdownTime / 60);
            int seconds = Mathf.FloorToInt(countdownTime % 60);

            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            // Jika waktu habis, load BeginnerStage
            SceneManager.LoadScene("BeginnerStage");
        }
    }
}
