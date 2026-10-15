using UnityEngine;

public class Pickup : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,0,240) * Time.deltaTime);
    }
}
