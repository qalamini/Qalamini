using TMPro;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveDuration;
    private Vector3 startPosition;
    private float elapsedTime = 0f;
    [SerializeField] private TextMeshPro harfText;

    public void Initialize(Vector3 targetPos, float duration, string currentHarfText)
    {
        startPosition = transform.position;
        targetPosition = targetPos;
        moveDuration = duration;
        elapsedTime = 0f;
        harfText.text = currentHarfText;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / moveDuration;

        transform.position = Vector3.Lerp(startPosition, targetPosition, t);

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
