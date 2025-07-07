using UnityEngine;

public class LineToImageConverter : MonoBehaviour
{
    public Camera drawingCamera; // Assign camera orthographic khusus LineRenderer
    public int imageSize = 32;

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

    public float[] ConvertTextureToInputArray(Texture2D tex)
    {
        Color32[] pixels = tex.GetPixels32();
        float[] input = new float[imageSize * imageSize];

        for (int i = 0; i < pixels.Length; i++)
        {
            Color32 c = pixels[i];
            // Convert ke grayscale dan normalisasi [0-1]
            float gray = (c.r + c.g + c.b) / 3f / 255f;
            input[i] = gray;
        }

        return input;
    }

    public float[] CaptureAndConvert()
    {
        Texture2D tex = CaptureLineAsTexture();
        return ConvertTextureToInputArray(tex);
    }
}
