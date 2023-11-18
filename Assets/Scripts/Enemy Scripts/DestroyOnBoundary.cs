using UnityEngine;

public class DestroyOnBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mengecek apakah objek yang bersentuhan adalah batas
        if (other.CompareTag("Boundary")||other.CompareTag("Player"))
        {
            // Hancurkan peluru ketika mencapai batas
            Destroy(gameObject);
        }
    }
}
