using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public Transform destino;
    public float pausa = 1.5f;
    public float alturaAparicion = 0.4f;

   
    static float tiempoLibre = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < tiempoLibre) return;

        tiempoLibre = Time.time + pausa;

        Vector3 nuevaPos = destino.position + Vector3.up * alturaAparicion;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null) rb.position = nuevaPos;
        other.transform.position = nuevaPos;

        Debug.Log("Teletransportado a " + destino.name);
    }
}