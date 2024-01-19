using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles moving the player rigidbody
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

    [SerializeField] private float _jumpModAmt = 0;
    [SerializeField] private float _speedMod = 0;

    private Rigidbody2D _rb;
    private CharacterController _controller;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<CharacterController>();

        _controller.OnJumpInput += TryJump;
    }

    private void OnDisable()
    {
        _controller.OnJumpInput -= TryJump;
    }


    private void FixedUpdate()
    {
        float speedCoefficient = _controller.HorizontalInput * (_movementSpeed+_speedMod) * Time.deltaTime;

        _rb.AddForce(Vector2.right * speedCoefficient);
    }

    private void TryJump()
    {
        //jump with jumpfoice - jumpMod
        _rb.AddForce((Vector2.up * (_jumpForce+_jumpModAmt)));
    }

    public void ChangeJumpMod(float amt)
    {
        _jumpModAmt = amt;
    }

    public void ChangeSpeedMod(float amt)
    {
        _speedMod = amt;
    }
}
