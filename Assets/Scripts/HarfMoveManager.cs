using TMPro;
using UnityEngine;

public class HarfMoveManager : MonoBehaviour
{
    // Class ini bertanggung jawab untuk mengelola pergerakan huruf (harf) di layar, 
    // termasuk spawn ulang huruf, mendeteksi tabrakan dengan batas tertentu, 
    // dan menangani aksi ketika pengguna menulis huruf dengan benar atau salah.


    [SerializeField] private GameObject harfPrefab;
    [SerializeField] private GameObject boundaryLine;
    [SerializeField] private GameObject trueIcon;
    [SerializeField] private GameObject falseIcon;

    [SerializeField] private float speed;
    private float collisionDistance = 2f;
    private GameObject harfObject;
    // private int countHarf = 0;

    private void Start()
    {
        ReSpawnHarf();
    }
    private void Update()
    {
        if (harfObject != null)
        {
            harfObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            CheckCollisionWithBoundary();
        }
        else if (harfObject == null && GameManager.currentHarfCount <= GameManager.harfLimit)
        {
            ReSpawnHarf();
        }
    }

    private void CheckCollisionWithBoundary()
    {
        if (boundaryLine != null)
        {
            float distance = Vector3.Distance(harfObject.transform.position, boundaryLine.transform.position);

            if (distance <= collisionDistance)
            {
                Debug.Log("Harf mencapai boundary!");
                OnHarfReachBoundary();
            }
        }
    }

    private void OnHarfReachBoundary()
    {
        // Aksi ketika mencapai boundary
        Debug.Log("Collision detected via distance check!");
        GameManager.life--;
        GameManager.isCorrect = false;
        Destroy(harfObject);
    }

    private void ReSpawnHarf()
    {
        if (harfObject == null)
        {
            harfObject = Instantiate(harfPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
            harfObject.GetComponentInChildren<TextMeshPro>().text = GameManager.currentHarf.character;
            Debug.Log("Harf object recreated.");
            GameManager.currentHarfCount++;
        }
    }

    public void OnWriteHarfCorrect()
    {
        if (harfObject != null)
        {
            Destroy(harfObject);
            harfObject = null;
            GameManager.isCorrect = true;
            GameManager.score += 1;
            Debug.Log("Harf object destroyed.");
            trueIcon.SetActive(true);
            Invoke("HideTrueIcon", 1f); // Sembunyikan ikon setelah 1 detik
        }
    }

    public void OnWriteHarfWrong()
    {
        if (harfObject != null)
        {
            // Destroy(harfObject);
            // harfObject = null;
            GameManager.isCorrect = false;
            // GameManager.life--;
            Debug.Log("Harf object destroyed due to wrong writing.");
            falseIcon.SetActive(true);
            Invoke("HideFalseIcon", 1f); // Sembunyikan ikon setelah 1 detik
        }
    }

    private void HideTrueIcon()
    {
        trueIcon.SetActive(false);
    }
    private void HideFalseIcon()
    {
        falseIcon.SetActive(false);
    }

}


