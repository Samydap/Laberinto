using UnityEngine;

public class CamaraPerseguir : MonoBehaviour
{
    public Transform jugador;
    public float distancia = 4f;
    public float altura = 1.0f;
    public float sensibilidad = 3f;
    public float velocidadTeclas = 100f;

    [Header("Colisión")]
    public float radioCamara = 0.15f;
    public float margen = 0.1f;
    public float distanciaMinima = 0.6f;
    public float suavizado = 8f;

    float yaw;
    float distanciaActual;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        distanciaActual = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensibilidad;
        if (Input.GetKey(KeyCode.Q)) yaw -= velocidadTeclas * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) yaw += velocidadTeclas * Time.deltaTime;

        Quaternion rot = Quaternion.Euler(0, yaw, 0);
        Vector3 pivote = jugador.position + Vector3.up * 0.3f;
        Vector3 deseada = jugador.position - rot * Vector3.forward * distancia + Vector3.up * altura;

        Vector3 dir = deseada - pivote;
        float distTotal = dir.magnitude;
        dir /= distTotal;

        // Cuánto puede alejarse la cámara sin chocar con algo
        float distLibre = distTotal;
        if (Physics.SphereCast(pivote, radioCamara, dir, out RaycastHit hit, distTotal,
                               ~0, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform != jugador)
                distLibre = Mathf.Max(hit.distance - margen, distanciaMinima);
        }

        // Si hay pared, se acerca de inmediato; si se libera, vuelve suavemente
        if (distLibre < distanciaActual)
            distanciaActual = distLibre;
        else
            distanciaActual = Mathf.Lerp(distanciaActual, distLibre, suavizado * Time.deltaTime);

        transform.position = pivote + dir * distanciaActual;
        transform.LookAt(pivote);
    }
}