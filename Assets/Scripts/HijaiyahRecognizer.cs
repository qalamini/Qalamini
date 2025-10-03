// using UnityEngine;
// using System.Collections.Generic;

// public class HijaiyahRecognizer : MonoBehaviour
// {
//     private Dictionary<string, Texture2D> templates;
//     private string[] labelMap = new string[] {
//         "alif","ba","ta","tsa","jim",
//         "ha","kha","dal","dzal","ra",
//         "zai","sin","syin","shad","dhad",
//         "tha","zha","ain","ghain","fa",
//         "qaf","kaf","lam","mim","nun",
//         "wawu","ha_marbuthah","ya"
//     };

//     void Start()
//     {
//         // Load semua template dari Resources/Templates
//         templates = new Dictionary<string, Texture2D>();
//         foreach (string huruf in labelMap)
//         {
//             Texture2D tex = Resources.Load<Texture2D>("Templates/" + huruf);
//             if (tex != null)
//             {
//                 templates.Add(huruf, tex);
//             }
//             else
//             {
//                 Debug.LogWarning("⚠️ Template tidak ditemukan untuk: " + huruf);
//             }
//         }
//     }

//     /// <summary>
//     /// Prediksi huruf berdasarkan template matching
//     /// </summary>
//     public string PredictHijaiyah(Texture2D inputTexture)
//     {
//         if (templates == null || templates.Count == 0)
//         {
//             Debug.LogError("❌ Template kosong! Pastikan sudah ada gambar di Resources/Templates/");
//             return "Unknown";
//         }

//         float bestScore = float.MinValue;
//         string bestMatch = "Unknown";

//         foreach (var kvp in templates)
//         {
//             float score = CompareTextures(inputTexture, kvp.Value);
//             if (score > bestScore)
//             {
//                 bestScore = score;
//                 bestMatch = kvp.Key;
//             }
//         }

//         Debug.Log($"Prediksi: {bestMatch} (score: {bestScore:F4})");
//         return bestMatch;
//     }

//     private float CompareTextures(Texture2D a, Texture2D b)
//     {
//         // Pastikan ukuran sama
//         if (a.width != b.width || a.height != b.height)
//         {
//             Debug.LogError("Ukuran gambar berbeda! Harus resize dulu.");
//             return -1f;
//         }

//         Color[] pixelsA = a.GetPixels();
//         Color[] pixelsB = b.GetPixels();
//         float score = 0f;

//         for (int i = 0; i < pixelsA.Length; i++)
//         {
//             float diff = Mathf.Abs(pixelsA[i].grayscale - pixelsB[i].grayscale);
//             score += 1f - diff; // semakin mirip, semakin tinggi nilainya
//         }

//         return score / pixelsA.Length; // normalisasi 0–1
//     }
// }



using Unity.Barracuda;
using UnityEngine;
using System.IO;

public class HijaiyahRecognizer : MonoBehaviour
{
    public NNModel modelAsset;
    private Model runtimeModel;
    private IWorker worker;

    void Awake()
    {
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);
    }


    public float PredictHijaiyah(string image)
    {
        string imagePath = image;
        Debug.Log(imagePath);

        // Load model
        // runtimeModel = ModelLoader.Load(modelAsset);
        // worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);

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

        // Output Array:
        return outputArray[0];

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
