using UnityEngine;

public class Anillo : MonoBehaviour
{
    [SerializeField] private int valor = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.puntajeTotal += valor;
        Debug.Log("Puntaje: " + GameManager.puntajeTotal);
        Destroy(gameObject);
    }
}