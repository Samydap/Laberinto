using UnityEngine;

public class Peligro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out Respawn respawn))
            respawn.Morir();
    }
}
