using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Salida : MonoBehaviour
{
    [SerializeField] private string escenaSiguiente = "Nivel2";

    [Header("Sonido")]
    public AudioClip sonidoSalida;
    [Range(0f, 1f)] public float volumen = 1f;
    public float esperaAntesDeCambiar = 1f;

    bool activada = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (activada) return;
        activada = true;

        if (sonidoSalida != null)
            AudioSource.PlayClipAtPoint(sonidoSalida, Camera.main.transform.position, volumen);

        StartCoroutine(Terminar());
    }

    IEnumerator Terminar()
    {
        yield return new WaitForSeconds(esperaAntesDeCambiar);

        if (!string.IsNullOrEmpty(escenaSiguiente))
        {
            Debug.Log("¡Nivel completado! Puntaje hasta ahora: " + GameManager.puntajeTotal);
            SceneManager.LoadScene(escenaSiguiente);
            yield break;
        }

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