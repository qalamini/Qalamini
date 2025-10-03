using UnityEngine;
using Unity.Barracuda;
using System.IO; // untuk File.ReadAllBytes

public class ONNXImageRunner : MonoBehaviour
{
    public NNModel modelAsset;
    private Model runtimeModel;
    private IWorker worker;

    // path ke gambar (misalnya Assets/StreamingAssets/test.png)
    private string imagePath;

    void Start()
    {
        imagePath = Path.Combine(Application.streamingAssetsPath, "ba.png");
        Debug.Log(Application.streamingAssetsPath);

        // Load model
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);

        // Load gambar dari file
        Texture2D tex = LoadGrayscaleImage(imagePath, 32, 32);

        // Konversi ke float array [0..1]
        float[] inputArray = TextureToFloatArray(tex);

        // Bungkus ke Tensor (NHWC: 1,32,32,1)
        Tensor input = new Tensor(1, 32, 32, 1, inputArray);

        // Eksekusi model
        worker.Execute(input);

        // Ambil hasil
        Tensor output = worker.PeekOutput();
        float[] outputArray = output.AsFloats();

        Debug.Log("=== Output Model ===");
        for (int i = 0; i < outputArray.Length; i++)
        {
            Debug.Log($"Class {i}: {outputArray[i]:F4}");
        }

        // Bersihkan
        input.Dispose();
        output.Dispose();
    }

    void OnDestroy()
    {
        worker.Dispose();
    }

    // Fungsi untuk load gambar grayscale dari file dan resize
    private Texture2D LoadGrayscaleImage(string path, int width, int height)
    {
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2, TextureFormat.RGB24, false);
        tex.LoadImage(fileData);
        Texture2D resizedTex = ResizeTexture(tex, width, height); // resize ke 32x32

        return resizedTex;
    }

    // Konversi Texture2D ke float array (grayscale normalized)
    private float[] TextureToFloatArray(Texture2D tex)
    {
        Color[] pixels = tex.GetPixels();
        float[] arr = new float[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            // Ambil grayscale = rata-rata R,G,B
            float gray = (pixels[i].r + pixels[i].g + pixels[i].b) / 3f;
            arr[i] = gray; // sudah 0..1
        }
        return arr;
    }

    // Resize Texture2D using RenderTexture
    private Texture2D ResizeTexture(Texture2D source, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D result = new Texture2D(width, height, source.format, false);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);
        return result;
    }
}
