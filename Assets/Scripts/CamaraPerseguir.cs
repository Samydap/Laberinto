using UnityEngine;

public class CamaraPerseguir : MonoBehaviour
{
    public Transform jugador;
    public Vector3 offset = new Vector3(0, 6, -4);

    void LateUpdate()
    {
        transform.position = jugador.position + offset;
        transform.LookAt(jugador);
    }
}