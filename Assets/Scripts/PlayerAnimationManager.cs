using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    public float speed = 0;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        speed = rb.linearVelocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
