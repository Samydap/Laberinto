using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float fuerzaSalto = 12f;
    [SerializeField] private Transform checkSuelo;
    [SerializeField] private float radioSuelo = 0.2f;
    [SerializeField] private LayerMask capaSuelo;

    private InputSystem_Actions controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool enSuelo;

    private void Awake()
    {
        controls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        enSuelo = Physics2D.OverlapCircle(checkSuelo.position, radioSuelo, capaSuelo);

        if (controls.Player.Jump.WasPressedThisFrame() && enSuelo)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * velocidad, rb.linearVelocity.y);
    }
}