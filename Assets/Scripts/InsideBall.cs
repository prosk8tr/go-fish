using UnityEngine;

public class InsideBall : MonoBehaviour
{
    public GameObject following;
    Vector3 offset = new Vector3(0f, 1.7f, 0f);

    void LateUpdate()
    {
        transform.position = following.transform.position - offset;
    }
}
