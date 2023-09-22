using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject _weaponProjectile;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private int _fireRate = 0;
    private Vector2 _projectileSpawnModifier;
    private Camera _cam;
    private CapsuleCollider2D _playerBoundingBox;

    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

    // Start is called before the first frame update
    void Awake()
    {
        //Initialize access to the main camera and player collision
        _cam = Camera.main;
        _playerBoundingBox = GetComponent<CapsuleCollider2D>();

        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * _movementSpeed * Time.deltaTime);
    }


    private void Update()
    {
        // Check if the player has fired a weapon recently
        if (_fireRate == 0)
        {
            if(Input.GetButton("Fire1"))
            {
                //Get values necessary to spawn projectiles outside of player collision
                _projectileSpawnModifier = Input.mousePosition - _cam.WorldToScreenPoint(_rb.position);
                _projectileSpawnModifier.Normalize();
                _projectileSpawnModifier = _projectileSpawnModifier * _playerBoundingBox.size;

                Instantiate(_weaponProjectile, _rb.position + _projectileSpawnModifier, Quaternion.identity);
                _fireRate = 30;
            }
        }
        else
        {
            _fireRate = _fireRate - 1;
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