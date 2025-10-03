using UnityEngine;

public class GamePlayManager : MonoBehaviour
{

    public LineToImageConverter lineToImageConverter;
    public HijaiyahRecognizer recognizer;
    public HarfMoveManager harfMoveManager;
    public TrueFalseSoundManager trueFalseSoundManager;

    private float writingTimer = 0f;
    private bool isWriting = false;

    void Update()
    {
        if (isWriting)
        {
            writingTimer += Time.deltaTime;

            if (writingTimer >= 1.5f)
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
        // string result = recognizer.PredictHijaiyah(GameManager.filePath);
        float probability = recognizer.PredictHijaiyah(GameManager.filePath);
        Debug.Log("CheckWriting - Predicted Hijaiyah letter: " + probability);

        if (probability >= 0.5f)
        {
            Debug.Log("Correct prediction!");
            harfMoveManager.OnWriteHarfCorrect();
            // kalau benar nyalakan suara benar
            trueFalseSoundManager.PlayCorrectSound();
        }
        else
        {
            Debug.Log("Incorrect prediction.");
            harfMoveManager.OnWriteHarfWrong();
            // kalau salah nyalakan suara salah
            trueFalseSoundManager.PlayWrongSound();
        }
    }
    
 

}

