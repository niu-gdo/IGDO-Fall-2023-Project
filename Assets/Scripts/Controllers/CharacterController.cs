using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

/// <summary>
/// Processes and propagates player input.
/// </summary>
public class CharacterController : MonoBehaviour
{
    public CharacterItemHandler _characterItemHandler
    {
        get
        {
            return GetComponent<CharacterItemHandler>();
        }
        private set { }
    }

    private Rigidbody2D _rb;
    private Vector2 _movement;

 

    public void OnShoot(InputValue value) => OnTriggerHeld?.Invoke(value.isPressed);

    public void OnJump(InputValue value) => OnJumpInput?.Invoke();
}
