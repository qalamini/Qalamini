using TMPro;
using UnityEngine;

public class HarfMoveManager : MonoBehaviour
{

    [SerializeField] private GameObject harfPrefab;
    [SerializeField] private GameObject boundaryLine;

    // bagaimana membuat speed nya lebih lambat lagi?
    // misalnya 0.0005f, tapi tidak terlalu lambat sehingga tidak terlihat
    // atau bisa juga menggunakan Time.deltaTime untuk mengatur kecepatan
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
        }
    }

    public void OnWriteHarfWrong()
    {
        if (harfObject != null)
        {
            Destroy(harfObject);
            harfObject = null;
            GameManager.isCorrect = false;
            GameManager.life--;
            Debug.Log("Harf object destroyed due to wrong writing.");
        }
    }

}


