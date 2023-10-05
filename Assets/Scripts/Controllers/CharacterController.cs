using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject _weaponProjectile; //Basic projectile prefab, spawned when player fires.
    private Rigidbody2D _rb;
    
    private Vector2 _movement;
    private float _fireRate = 0; //Used to control weapon firing speed.
    private Vector2 _projectileSpawnModifier; //The offset for spawning projectiles, and the direction they should move in after being fired.
    private Camera _cam; //Gives access to the main game camera
    private CapsuleCollider2D _playerBoundingBox; //Bounding box for the collision detection of the player.
    private bool _isTriggerHeld = false; //Tracks if the left mouse button is being held.
    private Rigidbody2D _projectileCollision; //Collision detection for the projectile fired by the current weapon. Used to move projectiles after firing.
    private float _bulletSpeed = 800f; //Speed of projectiles fired by current weapon; 800 is default value.
    private int _weaponHeld = 0; //Weapon currently in use by the player.

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

    private void TryFire()
    {
        //Handle the logic to determine what weapon to fire.
        switch(_weaponHeld)
        {
            default:
                //Get values necessary to spawn projectiles outside of player collision
                _projectileSpawnModifier = Input.mousePosition - _cam.WorldToScreenPoint(_rb.position);
                _projectileSpawnModifier.Normalize();
                _projectileSpawnModifier = _projectileSpawnModifier * _playerBoundingBox.size;

                //Spawn the weapon projectile.
                GameObject newProjectile = Instantiate(_weaponProjectile, _rb.position + _projectileSpawnModifier, Quaternion.identity);

                //Make the projectile move in the desired direction.
                _projectileSpawnModifier.Normalize();
                _projectileCollision = newProjectile.GetComponent<Rigidbody2D>();
                _projectileCollision.AddForce(_projectileSpawnModifier * _bulletSpeed);
            
                //Handle the firing rate.
                _fireRate = Time.time + 0.01f;
                break;
        }
    }

    void Update()
    {
        if(_isTriggerHeld && Time.time > _fireRate)
        {
            TryFire();
        }
    }

    private void OnShoot(InputValue value)
    {
        //If the button is pressed, flip to true. Once the button is released, flip to false.
        _isTriggerHeld = !_isTriggerHeld;
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