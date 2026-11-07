using UnityEngine;

public class AerialCameraController : MonoBehaviour
{
    public GameObject pivot;
    public float speed = 5f;
    public bool isActive;
    Vector3 startPosition;
    Quaternion startRotation;

    void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetCamera()
    {
        transform.SetPositionAndRotation(startPosition, startRotation);
    }

    void Update()
    {
        if (!isActive) return;

        transform.RotateAround(pivot.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
