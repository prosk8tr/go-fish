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

    public void Die()
    {
        audioSource.PlayOneShot(dieSound);
        ballCollider.enabled = false;
        StartCoroutine("RespawnPlayer");
    } 
    public void FreezeControls()
    {
        DisableMovement();
        cameraController.GetComponent<TiltInput>().tilt = Vector2.zero;
        cameraController.enabled = false;
    }

    public void DisableMovement()
    {
        enabled = false;
    }

    public void EnableControls()
    {
        enabled = true;
    }

    public void ResetToRespawn()
    {
        transform.position = RespawnPosition;
        StopBall();
        ResetOrientation();
        cameraController.enabled = true;
    }

    public void ResetOrientation()
    {
        calcYaw.ResetYaw();
        playerModel.ResetRotation();
        input.tilt = new Vector2(0,0);
        input.enabled = false;
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(dieWait);
        inventory.LoseLife();
        ballCollider.enabled = true;
        rb.transform.position = RespawnPosition;
        StopBall();
    }
    public void SlowDown()
    {
        rb.linearVelocity *= slowDownScale;
        rb.angularVelocity *= slowDownScale;
    }

    void StopBall()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
}
