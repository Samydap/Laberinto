using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float fuerzaSalto = 12f;
    [SerializeField] private Transform checkSuelo;
    [SerializeField] private float radioSuelo = 0.2f;
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D rb;
    private bool enSuelo;
    private float h;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        h = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) h -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) h += 1f;

        enSuelo = Physics2D.OverlapCircle(checkSuelo.position, radioSuelo, capaSuelo);

        if (kb.spaceKey.wasPressedThisFrame && enSuelo)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(h * velocidad, rb.linearVelocity.y);
    }
}