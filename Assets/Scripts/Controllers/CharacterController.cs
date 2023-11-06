using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;
    [SerializeField] private float _pickupRange = .5f;

    [SerializeField] private FacingDirection _direction;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * _movementSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();

        if(value.Get<Vector2>() == Vector2.right)
        {
            _direction = FacingDirection.Right;
        }else if(value.Get<Vector2>() == Vector2.left)
        {
            _direction = FacingDirection.Left;
        }
        _characterItemHandler.UpdateItemView();
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

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
        if (hit.collider != null && hit.collider.TryGetComponent<IInteractible>(out IInteractible interactible))
        {
            interactible.Interaction(this);
        }
    }

    public void OnDrop(InputValue value)
    {
        _characterItemHandler.DropItem();

    }

    public FacingDirection GetDirection()
    {
        return _direction;
    }
}
