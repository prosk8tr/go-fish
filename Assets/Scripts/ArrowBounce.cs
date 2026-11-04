using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ArrowBounce : MonoBehaviour { 
    public float speed = 5f;
    public float height = 0.5f;
    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
