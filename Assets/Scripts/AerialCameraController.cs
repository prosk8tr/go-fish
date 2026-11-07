using UnityEngine;

public class AerialCameraController : MonoBehaviour
{
    public GameObject pivot;
    public float speed = 5f;

    void Update()
    {
        transform.RotateAround(pivot.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
