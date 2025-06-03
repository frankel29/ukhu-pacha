using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f; // Duración antes de autodestruirse

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
