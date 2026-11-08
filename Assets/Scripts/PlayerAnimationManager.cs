using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        float speed = rb.linearVelocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
