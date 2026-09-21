using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida2D : MonoBehaviour
{
    [SerializeField] private string escenaSiguiente = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!string.IsNullOrEmpty(escenaSiguiente))
        {
            SceneManager.LoadScene(escenaSiguiente);
            return;
        }

        int siguiente = SceneManager.GetActiveScene().buildIndex + 1;

        if (siguiente < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguiente);
        }
        else
        {
            Debug.Log("¡Juego terminado! Puntaje final: " + GameManager.puntajeTotal);
        }
    }
}