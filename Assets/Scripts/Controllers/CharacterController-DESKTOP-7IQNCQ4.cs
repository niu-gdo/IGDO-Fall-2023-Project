using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private CapsuleCollider2D _cc;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

    // Start is called before the first frame update
    void Awake()
    {
        _cc = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * _movementSpeed * Time.deltaTime);
    }


    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public bool onGround()
    {   
        RaycastHit2D hit = Physics2D.Raycast(_cc.bounds.center, Vector2.down, _cc.bounds.extents.y + 0.1f,groundMask);
        return hit.collider != null;
    }
    public void OnJump(InputValue value)
    {
        if (onGround()){
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }
}
