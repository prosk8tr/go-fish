using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveFloor : MonoBehaviour
{
    Rigidbody stage;
    Vector2 targetTilt;
    Vector2 currentTilt;
    float sensitivity = 0.1f;
    float maxTilt = 25f;
    float smoothing = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stage = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetTilt += Mouse.current.delta.ReadValue() * sensitivity;
        targetTilt = new Vector2(
            Mathf.Clamp(targetTilt.x, -maxTilt, maxTilt),
            Mathf.Clamp(targetTilt.y, -maxTilt, maxTilt)
            );
    }

    void FixedUpdate()
    {
        float lerpTime = 1f - Mathf.Exp(-smoothing * Time.fixedDeltaTime);
        currentTilt = Vector2.Lerp(currentTilt, targetTilt, lerpTime);
        // https://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html
        stage.MoveRotation(Quaternion.Euler(currentTilt.y, 0f, -currentTilt.x));
    }
}

