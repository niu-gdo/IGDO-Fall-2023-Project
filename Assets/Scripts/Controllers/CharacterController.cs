using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;

    [SerializeField] private float _movementSpeed = 400f;
    [SerializeField] private float _jumpForce = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * _movementSpeed * Time.deltaTime);
        // Vector3 gravity = Vector3.down * (float)calcGravity();
        // _rb.AddForce(Vector3.down * (float)calcGravity());
    }


    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        float placeX = other.transform.position.x;
        float placeY = other.transform.position.y;

        _rb.AddForce(Vector3.down * (float)calcGravity(placeX, placeY));
    }

    double calcGravity(float placeX, float placeY)
    {
        double playerX = _rb.position.x;
        double playerY = _rb.position.y;

        double distance = Math.Pow(Math.Pow(placeX - playerX, 2) + Math.Pow(placeY - playerY, 2), 0.5);
        double maxGravity = 20;
    
        double gravity;
        if(distance > 0.1)
        {
            gravity = (1 / distance) * maxGravity;
        }
        else
        {
            gravity = (1 / 0.1) * maxGravity;
        }
        

        if(gravity > maxGravity)
        {
            gravity = maxGravity;
        }

        return gravity;
    }
}
