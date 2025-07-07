using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginnerStageController : MonoBehaviour
{
    [SerializeField] private Button[] buttonHarf;
    [SerializeField] private Button backButton;

    void Start()
    {
        for (int i = 0; i < buttonHarf.Length; i++)
        {
            int id = i + 1; 
            buttonHarf[i].onClick.AddListener(() => OnStageClicked(id));
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));
        }
    }

    void OnStageClicked(int harfId)
    {
        var harf = HijaiyahDatabase.GetLetterById(harfId);

        if (harf != null)
        {
            Debug.Log("OnStageClicked - Huruf yang dipilih: " + harf.name + " karakter: " + harf.character);
            GameManager.currentHarf = harf;
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            Debug.LogError("❌ Huruf dengan id " + harfId + " tidak ditemukan di database!");
        }
    }
}
