using UnityEngine;

public class FishParticleController : MonoBehaviour
{
    ParticleSystem particleSys;
    void Start()
    {
        particleSys = gameObject.GetComponent<ParticleSystem>();
    }
    public void EmitFishParticles(Transform trans)
    {
        Vector3 fishPosition = trans.transform.position;
        transform.position = fishPosition;
        particleSys.Play();
    }
}
