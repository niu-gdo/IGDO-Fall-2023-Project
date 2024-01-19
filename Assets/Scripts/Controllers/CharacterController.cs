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
        // Use a raycast to check if a pickup is in front of the player
        // ternary conditional is used to decide what direction to fire raycast based on _direction
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            (_direction == FacingDirection.Right) ? Vector2.right : Vector2.left,
            _pickupRange
        );

        //Debug.Log(hit.collider);

        //if interacable is found, interact with it
        if (hit.collider != null && hit.collider.CompareTag("Pickup"))
        {
            _characterItemHandler.PickupObject(hit.collider.gameObject);
        }
    }

    public void OnDrop(InputValue value)
    {
        _characterItemHandler.DropItem();

    }


    private void ProcessMovement(InputValue value)
    {
        _movement = value.Get<Vector2>();

        if (value.Get<Vector2>() == Vector2.right)
        {
            _direction = FacingDirection.Right;
        }
        else if (value.Get<Vector2>() == Vector2.left)
        {
            _direction = FacingDirection.Left;
        }
        _characterItemHandler.UpdateItemView();
    }


    public void OnShoot(InputValue value) => OnTriggerHeld?.Invoke(value.isPressed);
    public void OnMove(InputValue value) => ProcessMovement(value);
   

    public void OnJump(InputValue value) => OnJumpInput?.Invoke();
}
