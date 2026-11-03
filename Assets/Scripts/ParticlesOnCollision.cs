using UnityEngine;

public class ParticlesOnCollision : MonoBehaviour
{
    public ParticleSystem particleSys;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("AAA");
            ContactPoint contact = collision.contacts[0];

            particleSys.transform.position = contact.point;
            particleSys.transform.rotation = Quaternion.LookRotation(contact.normal);
            particleSys.Play();
        }
    }
}
