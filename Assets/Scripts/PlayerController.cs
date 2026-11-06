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
    public float dieWait = 1f;
    void Start()
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
        enabled = false;
        cameraController.GetComponent<TiltInput>().tilt = Vector2.zero;
        cameraController.enabled = false;
    }

    // Returns the player to the respawn point and restores player and camera input
    public void ResumeAtRespawn()
    {
        transform.position = RespawnPosition;
        cameraController.enabled = true;
        enabled = true;
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(dieWait);
        inventory.LoseLife();
        ballCollider.enabled = true;
        rb.transform.position = RespawnPosition;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
}
