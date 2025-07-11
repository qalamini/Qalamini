using UnityEngine;

public class HijaiyahTest : MonoBehaviour
{
    public HijaiyahRecognizer recognizer;

    void Start()
    {
        // Load image dummy dari Resources folder
        Texture2D testTexture = Resources.Load<Texture2D>("TestImages/bates");
        
        if (testTexture != null)
        {
            // Convert Texture2D ke float[] input model
            float[] inputArray = LineToImageConverter.ConvertToArray(testTexture);

            // Predict
            string result = recognizer.PredictHijaiyah(inputArray);

            Debug.Log("✅ Prediksi huruf: " + result);
        }
        else
        {
            Debug.LogError("❌ Gagal load test image");
        }
    }


}
