using UnityEngine;

public class Anillo : MonoBehaviour
{
    [SerializeField] private int valor = 1;

    [Header("Sonido (opcional)")]
    [SerializeField] private AudioClip sonidoRecoger;
    [SerializeField, Range(0f, 1f)] private float volumen = 1f;
    [SerializeField] private float variacionTono = 0.05f;

    private AudioSource fuente;
    private bool recogido;

    private void Awake()
    {
        if (!TryGetComponent(out fuente))
            fuente = gameObject.AddComponent<AudioSource>();

        fuente.playOnAwake = false;
        fuente.spatialBlend = 0f; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (recogido || !other.CompareTag("Player")) return;
        recogido = true;

        GameManager.puntajeTotal += valor;
        Debug.Log("Puntaje: " + GameManager.puntajeTotal);

        if (sonidoRecoger == null)
        {
            Destroy(gameObject);
            return;
        }


        foreach (var sr in GetComponentsInChildren<SpriteRenderer>()) sr.enabled = false;
        foreach (var c in GetComponents<Collider2D>()) c.enabled = false;

        fuente.pitch = 1f + Random.Range(-variacionTono, variacionTono);
        fuente.PlayOneShot(sonidoRecoger, volumen);
        Destroy(gameObject, sonidoRecoger.length / fuente.pitch + 0.1f);
    }
}