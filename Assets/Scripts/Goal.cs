using UnityEngine;

public class Goal : MonoBehaviour
{
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
            GameObject player = other.gameObject;
            player.SendMessage("SlowDown");
        }
    }
}
