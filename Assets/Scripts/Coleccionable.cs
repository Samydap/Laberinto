using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    public int puntos = 10;
    public GameObject particulas;

    [Header("Sonido")]
    public AudioClip sonido;
    [Range(0f, 1f)] public float volumen = 1f;

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.puntajeTotal += puntos;
        Debug.Log("Recogiste +" + puntos + " puntos. Total: " + GameManager.puntajeTotal);

        if (particulas != null)
        {
            GameObject fx = Instantiate(particulas, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(fx, 2f);
        }

        if (sonido != null)
            AudioSource.PlayClipAtPoint(sonido, transform.position, volumen);

        Destroy(gameObject);
    }
}