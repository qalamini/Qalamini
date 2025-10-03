using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "hijaiyahData.json");
        Debug.Log("Data path: " + filePath);
    }

    public void SaveDataHijaiyah(HijaiyahDataJson data)
    {
        string json = JsonUtility.ToJson(data, true); // true = biar rapi (pretty print)
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved: " + json);
    }

    public HijaiyahDataJson LoadDataHijaiyah()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HijaiyahDataJson data = JsonUtility.FromJson<HijaiyahDataJson>(json);
            Debug.Log("Data loaded: " + json);
            return data;
        }
        else
        {
            Debug.LogWarning("File not found, creating new data");
            return new HijaiyahDataJson { hijaiyah = new HijaiyahScore() };
        }
    }
}
