using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterShooting : MonoBehaviour
{
    [SerializeField] private GameObject _weaponProjectile; //Basic projectile prefab, spawned when player fires.

    private CharacterController _controller;

    private float _fireRate = 0; //Used to control weapon firing speed.
    private Vector2 _projectileSpawnModifier; //The offset for spawning projectiles, and the direction they should move in after being fired.
    private Camera _cam; //Gives access to the main game camera
    private CapsuleCollider2D _playerBoundingBox; //Bounding box for the collision detection of the player.
    private bool _isTriggerHeld = false; //Tracks if the left mouse button is being held.
    private Rigidbody2D _projectileCollision; //Collision detection for the projectile fired by the current weapon. Used to move projectiles after firing.
    private float _bulletSpeed = 800f; //Speed of projectiles fired by current weapon; 800 is default value.
    private int _weaponHeld = 0; //Weapon currently in use by the player.


    // Start is called before the first frame update
    void Awake()
    {
        //Initialize access to the main camera and player collision
        _cam = Camera.main;
        _playerBoundingBox = GetComponent<CapsuleCollider2D>();
        _controller = GetComponent<CharacterController>();

        _controller.OnTriggerHeld += OnTrigger;
    }

    private void OnDisable()
    {
        _controller.OnTriggerHeld -= OnTrigger;
    }

    private void TryFire()
    {
        //Handle the logic to determine what weapon to fire.
        switch (_weaponHeld)
        {
            default:
                //Get values necessary to spawn projectiles outside of player collision
                _projectileSpawnModifier = Input.mousePosition - _cam.WorldToScreenPoint(transform.position);
                _projectileSpawnModifier.Normalize();
                _projectileSpawnModifier = _projectileSpawnModifier * _playerBoundingBox.size;

                //Spawn the weapon projectile.
                GameObject newProjectile = Instantiate(_weaponProjectile, (Vector2)transform.position + _projectileSpawnModifier, Quaternion.identity);

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
        if (_isTriggerHeld && Time.time > _fireRate)
        {
            TryFire();
        }
    }

    private void OnTrigger(bool triggerHeld)
    {
        _isTriggerHeld = triggerHeld;
    }
}
