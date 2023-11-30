using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private bool _touchingButton = false;
    private GameObject _touchingButtonGameObject;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Button")
        {
            _touchingButton = true;
            _touchingButtonGameObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Button")
        {
            _touchingButton = false;
        }
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    public void OnInteract(InputValue value)
    {
        if (_touchingButton)
        {
            if (_touchingButtonGameObject.GetComponent<OuterDoorButtonController>().isPressed)
            {
                _touchingButtonGameObject.GetComponent<OuterDoorButtonController>().isPressed = false;
            }
            else
            {
                _touchingButtonGameObject.GetComponent<OuterDoorButtonController>().isPressed = true;
            }
        }
    }
}
