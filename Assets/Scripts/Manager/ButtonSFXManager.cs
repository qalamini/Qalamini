using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSFXManager : MonoBehaviour
{
    public static ButtonSFXManager Instance;

    [Header("Audio Source untuk SFX")]
    public AudioSource audioSource;

    [Header("Clip SFX Button Click")]
    public AudioClip buttonClickClip;

    void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // setiap scene load, panggil OnSceneLoaded
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddSFXToAllButtons();
    }

    // void AddSFXToAllButtons()
    // {
    //     Button[] buttons = FindObjectsOfType<Button>(true); // true untuk include inactive buttons
    //     foreach (Button button in buttons)
    //     {
    //         button.onClick.AddListener(() => PlayButtonClick());
    //     }
    // }

    void AddSFXToAllButtons()
    {
        // Menggunakan API baru FindObjectsByType
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayButtonClick());
        }
    }


    public void PlayButtonClick()
    {
        if (audioSource != null && buttonClickClip != null)
        {
            audioSource.PlayOneShot(buttonClickClip);
        }
    }
}

