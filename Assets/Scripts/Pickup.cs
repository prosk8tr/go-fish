using UnityEngine;

public class Pickup : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,0,240) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("fishPickup");
            //Particles
            Destroy(gameObject);
        }
    }
}
