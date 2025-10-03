using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginnerStageController : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private Button[] buttonHarf;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject[] starAlif;
    [SerializeField] private GameObject[] starBa;
    [SerializeField] private GameObject[] starTa;
    [SerializeField] private GameObject[] starTsa;
    [SerializeField] private GameObject[] starJim;
    [SerializeField] private GameObject[] starHa;
    [SerializeField] private GameObject[] starKho;
    [SerializeField] private GameObject[] starDal;

    void Start()
    {
        Debug.Log("BeginnerStageController - Loading data");
        var data = dataManager.LoadDataHijaiyah();
        Debug.Log("BeginnerStageController - Data loaded: " + JsonUtility.ToJson(data));
        Debug.Log("BeginnerStageController - Data loaded: " + data.hijaiyah.alif);
        RefreshUIHijaiyah();
        for (int i = 0; i < buttonHarf.Length; i++)
        {
            int id = i + 1;
            buttonHarf[i].onClick.AddListener(() => OnStageClicked(id));
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(() => SceneManager.LoadScene("LevelMenu"));
        }
    }

    void OnStageClicked(int harfId)
    {
        var harf = HijaiyahDatabase.GetLetterById(harfId);

        if (harf != null)
        {
            Debug.Log("OnStageClicked - Huruf yang dipilih: " + harf.name + " karakter: " + harf.character);
            GameManager.currentHarf = harf;
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            Debug.LogError("❌ Huruf dengan id " + harfId + " tidak ditemukan di database!");
        }
    }

    private void RefreshUIHijaiyah()
    {
        var data = dataManager.LoadDataHijaiyah();

        SetStars(starAlif, data.hijaiyah.alif);
        SetStars(starBa, data.hijaiyah.ba);
        SetStars(starTa, data.hijaiyah.ta);
        SetStars(starTsa, data.hijaiyah.tsa);
        SetStars(starJim, data.hijaiyah.jim);
        SetStars(starHa, data.hijaiyah.ha);
        SetStars(starKho, data.hijaiyah.kha);
        SetStars(starDal, data.hijaiyah.dal);

        // 🔒 Atur interaksi button sesuai progress
        // Button Alif selalu aktif
        buttonHarf[0].interactable = true;

        // Button Ba aktif hanya jika Alif >= 1 bintang
        buttonHarf[1].interactable = data.hijaiyah.alif >= 1;
        // coba buat agar sub object bernama "Lock" di dalam button nya akan set active jadi true
        buttonHarf[1].transform.Find("Lock").gameObject.SetActive(!buttonHarf[1].interactable);

        // Button Ta aktif hanya jika Ba >= 1 bintang
        buttonHarf[2].interactable = data.hijaiyah.ba >= 1;
        buttonHarf[2].transform.Find("Lock").gameObject.SetActive(!buttonHarf[2].interactable);

        // Button Tsa aktif hanya jika Ta >= 1 bintang
        buttonHarf[3].interactable = data.hijaiyah.ta >= 1;
        buttonHarf[3].transform.Find("Lock").gameObject.SetActive(!buttonHarf[3].interactable);

        // Button Jim aktif hanya jika Tsa >= 1 bintang
        buttonHarf[4].interactable = data.hijaiyah.tsa >= 1;
        buttonHarf[4].transform.Find("Lock").gameObject.SetActive(!buttonHarf[4].interactable);

        // Button Ha aktif hanya jika Jim >= 1 bintang
        buttonHarf[5].interactable = data.hijaiyah.jim >= 1;
        buttonHarf[5].transform.Find("Lock").gameObject.SetActive(!buttonHarf[5].interactable);

        // Button Kho aktif hanya jika Ha >= 1 bintang
        buttonHarf[6].interactable = data.hijaiyah.ha >= 1;
        buttonHarf[6].transform.Find("Lock").gameObject.SetActive(!buttonHarf[6].interactable);

        // Button Dal aktif hanya jika Kho >= 1 bintang
        buttonHarf[7].interactable = data.hijaiyah.kha >= 1;
        buttonHarf[7].transform.Find("Lock").gameObject.SetActive(!buttonHarf[7].interactable);
    }
    private void SetStars(GameObject[] stars, int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }
}
