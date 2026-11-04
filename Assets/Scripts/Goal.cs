using UnityEngine;

public class Goal : MonoBehaviour
{
    public float speedScale = 0.95f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("EndStage");
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().linearVelocity *= speedScale;
            other.GetComponent<Rigidbody>().angularVelocity *= speedScale;
        }
    }
}
