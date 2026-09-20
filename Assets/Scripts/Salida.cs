using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int siguiente = SceneManager.GetActiveScene().buildIndex + 1;

        if (siguiente < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("¡Nivel completado! Puntaje hasta ahora: " + GameManager.puntajeTotal);
            SceneManager.LoadScene(siguiente);
        }
        else
        {
            Debug.Log("¡Juego terminado! Puntaje final: " + GameManager.puntajeTotal);
        }
    }
}