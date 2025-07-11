using System.IO;
using UnityEngine;

public class LineToImageConverter : MonoBehaviour
{
    [SerializeField] private Camera drawingCamera; // Assign camera orthographic khusus LineRenderer
    private int imageSize = 720;

    private string folderName = "SavedDrawings";


    public Texture2D CaptureLineAsTexture()
    {
        // Buat RenderTexture ukuran model
        RenderTexture rt = new RenderTexture(imageSize, imageSize, 24);
        drawingCamera.targetTexture = rt;

        // Render camera ke RenderTexture
        drawingCamera.Render();

        // Convert ke Texture2D
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(imageSize, imageSize, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, imageSize, imageSize), 0, 0);
        tex.Apply();

        // Cleanup
        drawingCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        return tex;
    }


    public void CaptureAndSave()
    {
        // Capture LineRenderer menjadi Texture2D
        Texture2D texture = CaptureLineAsTexture();

        // Encode ke PNG
        byte[] bytes = texture.EncodeToPNG();

        // Tentukan path folder penyimpanan
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Buat nama file unik berdasarkan timestamp
        string fileName = "LineCapture_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(folderPath, fileName);
        GameManager.filePath = filePath;

        // Tulis file PNG
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Saved LineRenderer image to: " + filePath);
    }
    
    public static float[] ConvertToArray(Texture2D texture)
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
