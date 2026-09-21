using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaFinal : MonoBehaviour
{
    [SerializeField] private TMP_Text textoPuntaje;
    [SerializeField] private Button botonReiniciar;
    [SerializeField] private Button botonSalir;
    [SerializeField] private string escenaInicial = "Nivel 1";

    private void Start()
    {
        textoPuntaje.text = "Puntaje total: " + GameManager.puntajeTotal;
        botonReiniciar.onClick.AddListener(Reiniciar);
        botonSalir.onClick.AddListener(Salir);
    }

    private void Reiniciar()
    {
        GameManager.puntajeTotal = 0; 
        SceneManager.LoadScene(escenaInicial);
    }

    private void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
