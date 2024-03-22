using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

/// <summary>
/// Processes and propagates player input.
/// </summary>
/// <summary>
/// Processes and propagates player input.
/// </summary>
public class CharacterController : MonoBehaviour
{
    public Action OnJumpInput;
    public Action<bool> OnTriggerHeld;  // Propagates if the shoot button is held

    public float HorizontalInput { get => _movement.x; }

    private Rigidbody2D _rb;
    private Vector2 _movement;

 
 

    public void OnShoot(InputValue value) => OnTriggerHeld?.Invoke(value.isPressed);
    public void OnMove(InputValue value) => _movement = value.Get<Vector2>();

    public void OnJump(InputValue value) => OnJumpInput?.Invoke();
}
