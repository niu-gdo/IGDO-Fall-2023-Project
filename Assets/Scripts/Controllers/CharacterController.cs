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

 

    public void OnShoot(InputValue value) => OnTriggerHeld?.Invoke(value.isPressed);

    public void OnJump(InputValue value) => OnJumpInput?.Invoke();
}
