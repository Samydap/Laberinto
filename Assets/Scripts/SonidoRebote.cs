using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SonidoRebote : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoRebote;

    [Header("Ajustes")]
    [SerializeField] private float velocidadMinima = 1.5f;
    [SerializeField] private float velocidadMaxima = 12f;
    [SerializeField, Range(0f, 1f)] private float volumenMin = 0.3f;
    [SerializeField, Range(0f, 1f)] private float volumenMax = 1f;
    [SerializeField] private float variacionTono = 0.1f;

    private AudioSource fuente;

    private void Awake()
    {
        fuente = GetComponent<AudioSource>();
        fuente.playOnAwake = false;
        fuente.spatialBlend = 0f; // sonido 2D
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sonidoRebote == null) return;

        // Fuerza del impacto: los golpes muy suaves no suenan
        float impacto = collision.relativeVelocity.magnitude;
        if (impacto < velocidadMinima) return;

        // Más impacto = más volumen
        float t = Mathf.InverseLerp(velocidadMinima, velocidadMaxima, impacto);
        float volumen = Mathf.Lerp(volumenMin, volumenMax, t);

        // Pequeña variación de tono para que no suene siempre igual
        fuente.pitch = 1f + Random.Range(-variacionTono, variacionTono);
        fuente.PlayOneShot(sonidoRebote, volumen);
    }
}
