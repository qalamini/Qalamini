using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public LineToImageConverter lineToImageConverter;
    public HijaiyahRecognizer recognizer;
    public HarfMoveManager harfMoveManager;

    private float writingTimer = 0f;
    private bool isWriting = false;


    void Update()
    {
        if (isWriting)
        {
            writingTimer += Time.deltaTime;

            if (writingTimer >= 0.5f)
            {
                isWriting = false;
                writingTimer = 0f;

                // Simpan gambar dan panggil CheckWriting
                lineToImageConverter.CaptureAndSave();
                CheckWriting();
            }
        }
    }

    public void StartWriting()
    {
        isWriting = true;
        writingTimer = 0f;
    }


    private void CheckWriting()
    {

        // if (string.IsNullOrEmpty(GameManager.filePath))
        // {
        //     Debug.LogError("File path is empty. Please ensure a drawing is saved first.");
        //     return;
        // }

        // Texture2D texture = Resources.Load<Texture2D>(GameManager.filePath);
        // float[] inputArray = LineToImageConverter.ConvertToArray(texture);

        // string result = recognizer.PredictHijaiyah(inputArray);
        // Debug.Log("Predicted Hijaiyah letter: " + result);

        // if (result == GameManager.currentHarf.name) Debug.Log("Correct prediction!");
        // else Debug.Log("Incorrect prediction.");
        harfMoveManager.OnWriteHarfCorrect();
    }

}
