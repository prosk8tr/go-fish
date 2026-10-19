using UnityEngine;
public class FollowBall : MonoBehaviour
{
    public GameObject following;
    Vector3 offset = new Vector3(0f, 1.7f, 0f);

    //https://discussions.unity.com/t/stop-child-gameobject-rotating-when-parent-rotates/643020/5
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, following.transform.position - offset, 1);
    }
}
