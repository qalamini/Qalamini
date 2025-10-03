using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button closePauseButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI scoreResultWin;
    [SerializeField] private TextMeshProUGUI timerResultWin;
    [SerializeField] private TextMeshProUGUI scoreResultLose;
    [SerializeField] private TextMeshProUGUI timerResultLose;
    [SerializeField] private Image[] live;
    [SerializeField] private GameObject popupWin;
    [SerializeField] private GameObject popupLose;
    [SerializeField] private GameObject popupPause;
    [SerializeField] private GameObject popupSettings;
    [SerializeField] private Image[] stars;
    [SerializeField] private Button[] stageButton;
    [SerializeField] private Button[] homeButton;

    // [SerializeField] private ProgressManager progressManager;
    [SerializeField] private DataManager dataManager;
    private HijaiyahDataJson currentData;
    void Awake()
    {
        GameManager.ResetGame();
    }

    void Start()
    {
        TemplateButton(stageButton, "BeginnerStage");
        TemplateButton(homeButton, "MainMenu");
        pauseButton.onClick.AddListener(() => popupPause.SetActive(true));
        closePauseButton.onClick.AddListener(() => popupPause.SetActive(false));
        // infoButton.onClick.AddListener(() => popupSettings.SetActive(true));
        currentData = dataManager.LoadDataHijaiyah();

    }

    void Update()
    {
        // Waktu habis maka akan muncul popup lose
        // Waktu habis atau nyawa habis tapi score >= 10 maka akan muncul popup win
        // Point full maka akan muncul popup win
        if (GameManager.countdownTime <= 0 || GameManager.life <= 0)
        {
            if (GameManager.score >= 20)
            {
                // WIN CONDITION BINTANG 3
                GameManager.isWinning = true;
                popupWin.SetActive(true);
                stars[0].enabled = true;
                stars[1].enabled = true;
                stars[2].enabled = true;
                // progressManager.SaveHijaiyahProgress(GameManager.currentHarf.name, 3);
                AddScore(GameManager.currentHarf.name, 3);
                return;
            }
            else if (GameManager.score >= 15)
            {
                // WIN CONDITION BINTANG 2
                GameManager.isWinning = true;
                popupWin.SetActive(true);
                stars[0].enabled = true;
                stars[1].enabled = true;
                stars[2].enabled = false;
                // progressManager.SaveHijaiyahProgress(GameManager.currentHarf.name, 2);
                AddScore(GameManager.currentHarf.name, 2);
                return;
            }
            else if (GameManager.score >= 10)
            {
                // WIN CONDITION BINTANG 1
                GameManager.isWinning = true;
                popupWin.SetActive(true);
                stars[0].enabled = true;
                stars[1].enabled = false;
                stars[2].enabled = false;
                // progressManager.SaveHijaiyahProgress(GameManager.currentHarf.name, 1);
                AddScore(GameManager.currentHarf.name, 1);
                return;
            }
            else
            {
                // LOSE CONDITION
                GameManager.isLose = true;
                popupLose.SetActive(true);
                Debug.Log("Game Over. You lose!");
                // progressManager.SaveHijaiyahProgress(GameManager.currentHarf.name, 0);
                AddScore(GameManager.currentHarf.name, 0);
                return;
            }

        }
        else
        {
            GameStateUpdate();
        }

    }

    private void GameStateUpdate()
    {
        GameManager.countdownTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(GameManager.countdownTime / 60);
        int seconds = Mathf.FloorToInt(GameManager.countdownTime % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerResultWin.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerResultLose.text = string.Format("{0:00}:{1:00}", minutes, seconds);


        score.text = string.Format("{0}/20", GameManager.score.ToString());
        scoreResultWin.text = string.Format("{0}/20", GameManager.score.ToString());
        scoreResultLose.text = string.Format("{0}/20", GameManager.score.ToString());

        for (int i = 0; i < live.Length; i++)
        {
            if (i < GameManager.life) live[i].enabled = true;
            else live[i].enabled = false;
        }
    }

    private void TemplateButton(Button[] button, string sceneName)
    {
        foreach (Button btn in button)
        {
            btn.onClick.AddListener(() => SceneManager.LoadScene(sceneName));
        }
    }

    public void AddScore(string huruf, int score)
    {
        HijaiyahDataJson currentData = dataManager.LoadDataHijaiyah();
        // Update sesuai huruf
        switch (huruf)
        {
            case "alif":
                currentData.hijaiyah.alif = score;
                break;
            case "ba":
                currentData.hijaiyah.ba = score;
                break;
            case "ta":
                currentData.hijaiyah.ta = score;
                break;
            case "tsa":
                currentData.hijaiyah.tsa = score;
                break;
            case "jim":
                currentData.hijaiyah.jim = score;
                break;
            case "ha":
                currentData.hijaiyah.ha = score;
                break;
            case "kha":
                currentData.hijaiyah.kha = score;
                break;
            case "dal":
                currentData.hijaiyah.dal = score;
                break;
            case "dzal":
                currentData.hijaiyah.dzal = score;
                break;
            case "ra":
                currentData.hijaiyah.ra = score;
                break;
            case "zai":
                currentData.hijaiyah.zai = score;
                break;
            case "sin":
                currentData.hijaiyah.sin = score;
                break;
            case "syin":
                currentData.hijaiyah.syin = score;
                break;
            case "shad":
                currentData.hijaiyah.shad = score;
                break;
            case "dhad":
                currentData.hijaiyah.dhad = score;
                break;
            case "tha":
                currentData.hijaiyah.tha = score;
                break;
            case "zha":
                currentData.hijaiyah.zha = score;
                break;
            case "ain":
                currentData.hijaiyah.ain = score;
                break;
            case "ghain":
                currentData.hijaiyah.ghain = score;
                break;
            case "fa":
                currentData.hijaiyah.fa = score;
                break;
            case "qaf":
                currentData.hijaiyah.qaf = score;
                break;
            case "kaf":
                currentData.hijaiyah.kaf = score;
                break;
            case "lam":
                currentData.hijaiyah.lam = score;
                break;
            case "mim":
                currentData.hijaiyah.mim = score;
                break;
            case "nun":
                currentData.hijaiyah.nun = score;
                break;
            case "wawu":
                currentData.hijaiyah.wawu = score;
                break;
            case "ha_marbuthah":
                currentData.hijaiyah.ha_marbutoh = score;
                break;
            case "ya":
                currentData.hijaiyah.ya = score;
                break;
            default:
                Debug.LogWarning("Huruf tidak dikenali: " + huruf);
                break;
        }

        Debug.Log("AddScore - currentData: " + JsonUtility.ToJson(currentData));
        dataManager.SaveDataHijaiyah(currentData);
    }
}
