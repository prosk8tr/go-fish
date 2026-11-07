using UnityEngine;

public class AerialCameraController : MonoBehaviour
{
    public GameObject pivot;
    public float speed = 5f;
    public Vector3 resetPosition;
    public Quaternion resetRotation;
    public bool isActive;
    void ResetCamera()
    {
        pivot.transform.rotation = resetRotation;
        pivot.transform.position = resetPosition;
    }

    void Update()
    {
        if (!isActive) return;

        transform.RotateAround(pivot.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
