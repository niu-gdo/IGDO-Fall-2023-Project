using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles moving the player
/// </summary>

public class CharacterMovement : MonoBehaviour
{

    // Movement and Jump speeds
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    // The directional input for the character.
    private Vector2 _movementDirection;

    // Used to check if the character is on the ground
    [SerializeField] private Transform _groundCheck; // Invisible object used to detect ground.
    [SerializeField] private LayerMask _groundLayer; // Should be set to ground layer.

    // The rigid body of the player.
    private Rigidbody2D _rb;

    // Determine if the player is on the ground, this will allow the
    // player to jump again.
    private bool _isGrounded;

    private void Awake()
    {
        // Get the rigidbody2d of the player
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    { 
        // Check if the player is on the ground.
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer);

        // Move the character
        MoveCharacter();

    }


    /// <summary>
    /// MoveCharacter
    /// 
    /// Move the character in the direction of the inputs.
    /// </summary>
    private void MoveCharacter()
    {
        // Apply movement directly to the transform
        transform.Translate(_movementDirection * _moveSpeed * Time.deltaTime);
    }


    /// <summary>
    /// OnMove
    /// 
    /// Sets the direction of the movement from the values given by the controls.
    /// </summary>
    /// <param name="value">
    /// The values of the control. This is automatic from the
    /// new InputSystem
    /// </param>
    public void OnMove(InputValue value)
    {
        // Read input value
        _movementDirection = value.Get<Vector2>();
    }


    /// <summary>
    /// OnJump
    /// 
    /// Allows the Character to jump. It does this by applying a force upward on
    /// the characters rigidbody2D.
    /// </summary>
    /// <param name="value">This has no use, but is required.</param>
    public void OnJump(InputValue value)
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }
}