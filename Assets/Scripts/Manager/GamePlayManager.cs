using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GameObject harfPrefab;
    [SerializeField] private Transform spawnPoint;   // posisi awal (kiri)
    [SerializeField] private Transform targetPoint;  // posisi akhir (kanan)
    private float moveDuration = 8f;
    private float spawnInterval = 6f;

    private float timer = 0f;

    // void Start()
    // {
    //     Vector3 leftMiddle = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane + 10));
    //     spawnPoint.position = leftMiddle;
    // }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnHarf();
            timer = 0f;
        }
    }

    void SpawnHarf()
    {
        GameObject harf = Instantiate(harfPrefab, spawnPoint.position, Quaternion.identity);
        MoveObject moveScript = harf.GetComponent<MoveObject>();
        moveScript.Initialize(targetPoint.position, moveDuration, GameManager.currentHarf.character);
    }
}
