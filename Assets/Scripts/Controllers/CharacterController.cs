using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;

    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * _movementSpeed * Time.deltaTime);
        Vector3 gravity = Vector3.down * (float)calcGravity();
        _rb.AddForce(Vector3.down * (float)calcGravity());
    }


    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    double calcGravity()
    {
        double playerX = _rb.position.x;
        double playerY = _rb.position.y;

        double distance = Math.Pow(Math.Pow(4 - playerX, 2) + Math.Pow(-3 - playerY, 2), 0.5);

        return (1/distance)*7;
    }
}
