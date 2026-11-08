using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    float speed = 0;
    public float speedScale = 1;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        speed = rb.linearVelocity.magnitude;
        animator.SetFloat("Speed", speed);
        float multiplier = speed > 0.1f ? Mathf.Clamp(speed / speedScale, 0.5f, 2f) : 1f;
        animator.SetFloat("SpeedMultiplier", multiplier);
    }
}
