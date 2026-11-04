using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TiltInput input;
    float force = 20f; // max applicable force, in m/s
    Rigidbody rb;
    public CalculateYaw calcYaw;
    AudioSource audioSource;
    public AudioClip dieSound;
    public Inventory inventory;
    public Vector3 RespawnPosition { get; set; }
    public Collider ballCollider;
    public float dieWait = 1f;
    public TextMeshProUGUI stageClearText;
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

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(dieWait);
        inventory.SendMessage("LoseLife");
        ballCollider.enabled = true;
        rb.transform.position = RespawnPosition;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }

    void EndStage()
    {
        stageClearText.gameObject.SetActive(true);
        inventory.SendMessage("CalcScore");
    }
}
