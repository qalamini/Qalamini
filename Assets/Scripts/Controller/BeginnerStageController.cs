using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginnerStageController : MonoBehaviour
{
    [SerializeField] private Button[] buttonHarf;
    [SerializeField] private Button alifButton;
    [SerializeField] private Button baButton;
    [SerializeField] private Button taButton;
    [SerializeField] private Button tsaButton;
    [SerializeField] private Button jimButton;
    [SerializeField] private Button haButton;
    [SerializeField] private Button khaButton;
    [SerializeField] private Button dalButton;
    [SerializeField] private Button backButton;

    void Start()
    {
        for (int i = 0; i < buttonHarf.Length; i++) buttonHarf[i].onClick.AddListener(() => SceneManager.LoadScene("GamePlay"));
        if (backButton != null) backButton.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));

        alifButton.onClick.AddListener(() => OnStageClicked("Alif"));
        baButton.onClick.AddListener(() => OnStageClicked("Ba"));
        taButton.onClick.AddListener(() => OnStageClicked("Ta"));
        tsaButton.onClick.AddListener(() => OnStageClicked("Tsa"));
        jimButton.onClick.AddListener(() => OnStageClicked("Jim"));
        haButton.onClick.AddListener(() => OnStageClicked("Ha"));
        khaButton.onClick.AddListener(() => OnStageClicked("Kha"));
        dalButton.onClick.AddListener(() => OnStageClicked("Dal"));
    }

    void OnStageClicked(string harfName)
    {
        // Ambil data huruf dari database
        var harf = HijaiyahDatabase.GetLetterByName(harfName);
        Debug.Log("OnStagedClicked - Huruf yang dipilih: " + harf.name + " karakter: " + harf.character);

        // Simpan ke GameManager untuk digunakan di gameplay scene
        GameManager.currentHarf = harf;
        // Load gameplay scene
        SceneManager.LoadScene("GamePlay");
    }

}
