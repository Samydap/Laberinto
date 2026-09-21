using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public AudioClip sonidoChoque;

    Rigidbody rb;
    AudioSource audioSource;
    Transform cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        
        Vector3 adelante = cam.forward;
        adelante.y = 0;
        adelante.Normalize();

        Vector3 derecha = cam.right;
        derecha.y = 0;
        derecha.Normalize();

        rb.AddForce((adelante * v + derecha * h) * speed);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Pared"))
            audioSource.PlayOneShot(sonidoChoque);
    }
}




