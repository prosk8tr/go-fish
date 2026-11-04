using UnityEngine;

public class PickupParticleController : MonoBehaviour
{
    ParticleSystem particleSys;
    void Start()
    {
        particleSys = gameObject.GetComponent<ParticleSystem>();
    }
    void emitFishParticles(Transform trans)
    {
        Vector3 fishPosition = trans.transform.position;
        transform.position = fishPosition;
        particleSys.Play();
    }
}
