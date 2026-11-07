using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TiltInput input;
    public float force = 20f; // max applicable force, in m/s
    Rigidbody rb;
    public CalculateYaw calcYaw;
    AudioSource audioSource;
    public AudioClip dieSound;
    public GameManager gameManager;
    public Vector3 RespawnPosition { get; set; }
    public Collider ballCollider;
    public CameraController cameraController;
    public RotatePlayerModel playerModel;
    public float dieWait = 1f;
    public float slowDownScale = 0.95f;
    // Awake rather than Start, so setup still runs if movement is disabled before the first frame
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        RespawnPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 tiltStrength = input.TiltStrength;
        Vector3 movement = new Vector3(tiltStrength.x, 0f, tiltStrength.y);
        Quaternion direction = Quaternion.Euler(0f, calcYaw.Yaw, 0f);

        rb.AddForce(direction * movement * force, ForceMode.Acceleration);
    }

    // The player is only in play while controls are enabled (not during the countdown, timeout, stage clear or game over)
    public bool InPlay => enabled;

    public void Die()
    {
        if (!InPlay) return; // invulnerable while not in play
        audioSource.PlayOneShot(dieSound);
        gameManager.timer.StopTimer();
        ballCollider.enabled = false;
        StartCoroutine("RespawnPlayer");
    } 
    public void FreezeControls()
    {
        DisableMovement();
    }

    // Stops the player responding to input. The tilt eases back to neutral, as if the mouse was released
    public void DisableMovement()
    {
        enabled = false;
        input.acceptInput = false;
        calcYaw.frozen = true;
    }

    public void EnableControls()
    {
        input.acceptInput = true;
        calcYaw.frozen = false;
        enabled = true;
    }

    public void ResetToRespawn()
    {
        TeleportTo(RespawnPosition);
        StopBall();
        ResetOrientation();
        cameraController.enabled = true;
    }

    public void ResetOrientation()
    {
        calcYaw.ResetYaw();
        playerModel.ResetRotation();
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(dieWait);
        gameManager.LoseLife();
        if (gameManager.OutOfLives)
        {
            gameManager.GameOver();
            yield break; // no respawn -- the run is over
        }
        ballCollider.enabled = true;
        TeleportTo(RespawnPosition);
        StopBall();
        gameManager.timer.ResetTimer(); // the clock always restarts from the full stage time
        gameManager.timer.StartTimer();
    }
    public void SlowDown()
    {
        rb.linearVelocity *= slowDownScale;
        rb.angularVelocity *= slowDownScale;
    }

    // Moves the ball through the Rigidbody. Setting transform.position alone can be overwritten by
    // interpolation before the physics step picks it up, leaving the ball where it was.
    void TeleportTo(Vector3 position)
    {
        rb.position = position;
        transform.position = position;
        Physics.SyncTransforms();
    }

    void StopBall()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
}
