using UnityEngine;

public class TrampaInvisible : MonoBehaviour
{
    public Transform puntoInicio;
    public bool reiniciarPuntaje = true;

    void OnTriggerEnter(Collider other)
    {
        Activar(other.gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        Activar(c.gameObject);
    }

    void Activar(GameObject obj)
    {
        if (!obj.CompareTag("Player")) return;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = puntoInicio.position;
        }
        obj.transform.position = puntoInicio.position;

        if (reiniciarPuntaje)
            GameManager.puntajeTotal = 0;

        Debug.Log("¡Caíste en una trampa! Vuelves al inicio. Puntaje: " + GameManager.puntajeTotal);
    }
}