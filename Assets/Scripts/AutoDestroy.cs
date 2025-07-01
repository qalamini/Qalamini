using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float lifetime = 4f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}