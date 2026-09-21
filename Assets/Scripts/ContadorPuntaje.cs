using TMPro;
using UnityEngine;

public class ContadorPuntaje : MonoBehaviour
{
    [SerializeField] private TMP_Text texto;

    private void Awake()
    {
        if (texto == null) texto = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        texto.text = "Puntaje: " + GameManager.puntajeTotal;
    }
}