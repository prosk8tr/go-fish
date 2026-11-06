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
    public Inventory inventory;
    public Vector3 RespawnPosition { get; set; }
    public Collider ballCollider;
    public CameraController cameraController;
    public RotatePlayerModel playerModel;
    public float dieWait = 1f;
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

    void Die()
    {
        audioSource.PlayOneShot(dieSound);
        ballCollider.enabled = false;
        StartCoroutine("RespawnPlayer");
    }

    // Stops player and camera input (e.g. when the stage is cleared)
    public void FreezeControls()
    {
        DisableMovement();
        cameraController.GetComponent<TiltInput>().tilt = Vector2.zero;
        cameraController.enabled = false;
    }

    // Stops the player responding to input (the camera is unaffected)
    public void DisableMovement()
    {
        enabled = false;
    }

    // Lets the player respond to input again
    public void EnableControls()
    {
        enabled = true;
    }

    // Returns the ball to the respawn point, at rest, with the camera following it again.
    // Movement stays disabled until EnableControls is called.
    public void ResetToRespawn()
    {
        transform.position = RespawnPosition;
        StopBall();
        ResetOrientation();
        cameraController.enabled = true;
    }

    // Resets the camera yaw and player model rotation, e.g. when starting the next stage
    public void ResetOrientation()
    {
        calcYaw.ResetYaw();
        playerModel.ResetRotation();
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(dieWait);
        inventory.LoseLife();
        ballCollider.enabled = true;
        rb.transform.position = RespawnPosition;
        StopBall();
    }

    void StopBall()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
}
