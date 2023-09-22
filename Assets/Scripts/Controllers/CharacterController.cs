using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public GameObject weaponProjectile;
    private Rigidbody2D _rb;
    private Vector2 _movement;

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

        // Check if the player is firing a weapon
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(weaponProjectile, _rb.position, Quaternion.identity);
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
}