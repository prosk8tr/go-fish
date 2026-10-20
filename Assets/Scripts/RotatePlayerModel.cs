using UnityEngine;

public class RotatePlayerModel : MonoBehaviour
{
    public PlayerController player;
    public float turnSpeed = 300f;
    void LateUpdate()
    {
        Vector3 direction = player.ForceDirection;
        direction.y = 0f;
        Quaternion target = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);
    }
}
