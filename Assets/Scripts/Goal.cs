using UnityEngine;

public class Goal : MonoBehaviour
{
    public float speedScale = 0.95f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().EndStage();
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.linearVelocity *= speedScale;
            rb.angularVelocity *= speedScale;
        }
    }
}
