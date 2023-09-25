using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private Vector2 _movement;

    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

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
        // check if vector2.up is present, we don't use that yet so we odn't need it at all
        if (value.Get<Vector2>().y > 0)
        {
            //if up is present, reset y to 0
            Vector2 move = value.Get<Vector2>();
            move.y = 0;
            _movement = move;
        }
        else
        {
            _movement = value.Get<Vector2>();
        }
        
        //Debug.Log(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }
}
