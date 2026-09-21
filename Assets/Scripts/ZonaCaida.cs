using UnityEngine;

public class ZonaCaida : MonoBehaviour
{
    public GameObject plataformas;
    public AudioClip sonidoDesaparece;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (sonidoDesaparece != null)
            AudioSource.PlayClipAtPoint(sonidoDesaparece, transform.position);

        plataformas.SetActive(false);
        Destroy(gameObject);
    }
}