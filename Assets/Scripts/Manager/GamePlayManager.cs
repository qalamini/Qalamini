using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GameObject harfPrefab;
    [SerializeField] private Transform spawnPoint;   // posisi awal (kiri)
    [SerializeField] private Transform targetPoint;  // posisi akhir (kanan)
    private float moveDuration = 8f;
    private float spawnInterval = 6f;

    private float timer = 0f;

    public LineToImageConverter lineToImageConverter;
    public HijaiyahRecognizer recognizer;

    [SerializeField] public Button checkAnswer;

    // void Start()
    // {
    //     Vector3 leftMiddle = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane + 10));
    //     spawnPoint.position = leftMiddle;
    // }

    void Start()
    {
        checkAnswer.onClick.AddListener(() => CheckAnswer());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnHarf();
            timer = 0f;
        }
    }

    void SpawnHarf()
    {
        GameObject harf = Instantiate(harfPrefab, spawnPoint.position, Quaternion.identity);
        MoveObject moveScript = harf.GetComponent<MoveObject>();
        moveScript.Initialize(targetPoint.position, moveDuration, GameManager.currentHarf.character);
    }

    public void CheckAnswer()
    {
        // Capture LineRenderer ke float[]
        float[] inputArray = lineToImageConverter.CaptureAndConvert();

        // Prediksi hurufnya
        string result = recognizer.PredictHijaiyah(inputArray);

        // Tampilkan hasil ke console
        Debug.Log("✅ CheckAnswer - Hasil prediksi huruf: " + result);

        // TODO: bandingkan dengan currentHarf dari GameManager
        if(result == GameManager.currentHarf.name)
        {
            Debug.Log("✅ CheckAnswer - Jawaban Benar!");
        }
        else
        {
            Debug.Log("❌ CheckAnswer - Jawaban Salah.");
        }
    }
}
