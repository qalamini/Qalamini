using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginnerStageController : MonoBehaviour
{
    [SerializeField] private Button[] buttonHarf;
    [SerializeField] private Button backButton;

    void Start()
    {
        for (int i = 0; i < buttonHarf.Length; i++) buttonHarf[i].onClick.AddListener(() => SceneManager.LoadScene("GamePlay"));
        if (backButton != null ) backButton.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));
    }

}
