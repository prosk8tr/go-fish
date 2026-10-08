using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveFloor : MonoBehaviour
{
    Rigidbody stage;
    float pitch, roll;
    float sensitivity = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stage = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        pitch += mouseDelta.y * sensitivity;
        roll -= mouseDelta.x * sensitivity;
    }

    void FixedUpdate()
    {
        // https://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html
        stage.MoveRotation(Quaternion.Euler(pitch, 0f, roll));
    }
}

