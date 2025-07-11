using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectCobaScript : MonoBehaviour
{
    [SerializeField] private Button buttonCoba;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("hello Worl");
        buttonCoba.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hello Worlasdsadas");
    }
}
