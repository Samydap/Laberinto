using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 puntoRespawn;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        puntoRespawn = transform.position;
    }

    public void SetCheckpoint(Vector3 posicion)
    {
        puntoRespawn = posicion;
    }

    public void Morir()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = puntoRespawn;
    }
}