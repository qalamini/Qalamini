using Unity.Barracuda;
using UnityEngine;

public class HijaiyahRecognizer : MonoBehaviour
{
    public NNModel modelAsset;
    private Model runtimeModel;
    private IWorker worker;

    void Start()
    {
        if (modelAsset == null)
        {
            Debug.LogError("❌ modelAsset belum diassign!");
            return;
        }

        runtimeModel = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);
    }

    public string PredictHijaiyah(float[] inputArray)
    {
        if (worker == null)
        {
            Debug.LogError("❌ Worker Barracuda null!");
            return "Unknown";
        }

        // Pastikan inputArray panjangnya 32*32 = 1024
        Tensor input = new Tensor(1, 32, 32, 1, inputArray);

        worker.Execute(input);

        // Print semua nama output
        foreach (var n in runtimeModel.outputs)
            Debug.Log("Model Output: " + n);

        Tensor output = worker.PeekOutput();
        if (output == null)
        {
            Debug.LogError("❌ Output tensor null. Cek nama output dan shape input.");
            input.Dispose();
            return "Unknown";
        }

        // int predictedIndex = output.ArgMax()[0];

         // Dapatkan array output mentah
        float[] outputArray = output.AsFloats();

        // Log seluruh output ke console
        Debug.Log("=== Output mentah model ===");
        for (int i = 0; i < outputArray.Length; i++)
        {
            if (outputArray[i] > 0.3f)
            {
                Debug.Log($"Class {i}: {outputArray[i]:F4}");
            }
        }

        // Pilih index dengan nilai paling tinggi
        int predictedIndex = output.ArgMax()[0];


        input.Dispose();
        output.Dispose();

        string[] labelMap = new string[] {
            "alif","ba","ta","tsa","jim",
            "ha","kha","dal","dzal","ra",
            "zai","sin","syin","shad","dhad",
            "tha","zha","ain","ghain","fa",
            "qaf","kaf","lam","mim","nun",
            "wawu","ha_marbuthah","ya"
        };

        return labelMap[predictedIndex];
    }


    void OnDestroy()
    {
        worker.Dispose();
    }
}
