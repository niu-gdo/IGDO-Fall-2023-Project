using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Processes and propagates player input.
/// </summary>
public class CharacterController : MonoBehaviour
{
    public Action OnJumpInput;
    public Action<bool> OnTriggerHeld;  // Propagates if the shoot button is held
    public float HorizontalInput { get => _movement.x; }

    public CharacterItemHandler _characterItemHandler
    {
        get
        {
            return GetComponent<CharacterItemHandler>();
        }
        private set { }
    }

    private Vector2 _movement;

    [SerializeField] private float _pickupRange = .5f;

    private FacingDirection _direction = FacingDirection.Right;

    public FacingDirection Direction{ get => _direction; }

    public void OnInteract(InputValue value)
    {

        _movement = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }
}
