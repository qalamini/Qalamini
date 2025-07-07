using UnityEngine;

public class HijaiyahTest : MonoBehaviour
{
    public HijaiyahRecognizer recognizer;

    void Start()
    {
        // Load image dummy dari Resources folder
        Texture2D testTexture = Resources.Load<Texture2D>("TestImages/ba");
        
        if (testTexture != null)
        {
            // Convert Texture2D ke float[] input model
            float[] inputArray = PreprocessTexture(testTexture);

            // Predict
            string result = recognizer.PredictHijaiyah(inputArray);

            Debug.Log("✅ Prediksi huruf: " + result);
        }
        else
        {
            Debug.LogError("❌ Gagal load test image");
        }
    }

    float[] PreprocessTexture(Texture2D texture)
    {
        // Buat RenderTexture untuk resize
        RenderTexture rt = RenderTexture.GetTemporary(32, 32, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        // Blit (copy) texture ke RenderTexture
        Graphics.Blit(texture, rt);

        // Set active RT dan baca ke Texture2D baru
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D resized = new Texture2D(32, 32, TextureFormat.RGB24, false);
        resized.ReadPixels(new Rect(0, 0, 32, 32), 0, 0);
        resized.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        // Convert ke grayscale dan normalisasi [0-1]
        Color32[] pixels = resized.GetPixels32();
        float[] result = new float[32 * 32];

        for (int i = 0; i < pixels.Length; i++)
        {
            Color32 c = pixels[i];
            float gray = (c.r + c.g + c.b) / 3f / 255f;
            result[i] = gray;
        }

        return result;
    }

}
