using UnityEngine;
using UnityEngine.InputSystem;

public class BounceController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 6f;

    [Header("Rebote automático")]
    [SerializeField] private bool rebotarSolo = true;
    [SerializeField] private float fuerzaRebote = 6f;
    [SerializeField] private float fuerzaReboteAlto = 9f;

    [Header("Frenar el rebote (mantener S / Flecha abajo / Shift)")]
    [SerializeField, Range(0.1f, 0.95f)] private float factorAmortiguacion = 0.65f;
    [SerializeField] private float fuerzaMinima = 2.5f;

    [Header("Salto (si Rebotar Solo está apagado)")]
    [SerializeField] private float fuerzaSalto = 12f;

    [Header("Saltos en el aire")]
    [SerializeField] private int saltosAereosMax = 1;
    [SerializeField] private float fuerzaSaltoAereo = 10f;

    [Header("Detección de suelo")]
    [SerializeField] private Transform checkSuelo;
    [SerializeField] private float radioSuelo = 0.2f;
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D rb;
    private float h;
    private bool saltoMantenido;
    private bool saltoPedido;
    private bool amortiguando;
    private int saltosAereosRestantes;
    private float fuerzaActual;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fuerzaActual = fuerzaRebote;
    }

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        h = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) h -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) h += 1f;

        saltoMantenido = kb.spaceKey.isPressed;
        if (kb.spaceKey.wasPressedThisFrame) saltoPedido = true;

        amortiguando = kb.sKey.isPressed || kb.downArrowKey.isPressed || kb.leftShiftKey.isPressed;
    }

    private void FixedUpdate()
    {
        Vector2 v = rb.linearVelocity;
        v.x = h * velocidad;

        bool tocandoSuelo = Physics2D.OverlapCircle(checkSuelo.position, radioSuelo, capaSuelo)
                            && v.y <= 0.1f;

        if (tocandoSuelo)
        {
            saltosAereosRestantes = saltosAereosMax;

            if (rebotarSolo)
            {
                float fuerzaBase = saltoMantenido ? fuerzaReboteAlto : fuerzaRebote;

                if (amortiguando && !saltoPedido)
                {
                    // Cada rebote es más bajo; por debajo del mínimo se queda quieta
                    fuerzaActual *= factorAmortiguacion;
                    if (fuerzaActual < fuerzaMinima) fuerzaActual = 0f;
                }
                else
                {
                    fuerzaActual = fuerzaBase;
                }

                if (fuerzaActual > 0f) v.y = fuerzaActual;
            }
            else if (saltoPedido)
            {
                v.y = fuerzaSalto;
            }
        }
        else if (saltoPedido && saltosAereosRestantes > 0)
        {
            v.y = fuerzaSaltoAereo;
            saltosAereosRestantes--;
        }

        saltoPedido = false;
        rb.linearVelocity = v;
    }
}