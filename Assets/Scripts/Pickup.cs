using UnityEngine;

public class Pickup : MonoBehaviour
{
    public FishParticleController particleSys;
    void Update()
    {
        transform.Rotate(new Vector3(0,0,240) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().FishPickup();
            particleSys.EmitFishParticles(transform);
            Destroy(gameObject);
        }
    }
}
