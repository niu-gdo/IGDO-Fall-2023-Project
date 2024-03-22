using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

/// <summary>
/// Processes and propagates player input.
/// </summary>
/// <summary>
/// Processes and propagates player input.
/// </summary>
public class CharacterController : MonoBehaviour
{
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
    private IObjectPool<ProjectileController> projectilePool; // stack-based ObjectPool for projectiles.

    [SerializeField] private GameObject defaultProjectile;
    [SerializeField] private bool collectionCheck = true; //Throw an exception if we try to return an existing projectile, already in the pool
    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity = 15;
    [SerializeField] private int maxSize = 15;

    // Start is called before the first frame update
    void Awake()
    {
        //Initialize access to the main camera and player collision
        _cam = Camera.main;
        _playerBoundingBox = GetComponent<CapsuleCollider2D>();

        _rb = GetComponent<Rigidbody2D>();

        projectilePool = new ObjectPool<ProjectileController>(CreateProjectile,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize); //Instantiate the projectile pool.
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

                //Retrieve a pooled weapon projectile.
                ProjectileController bulletObject = projectilePool.Get();

                if (bulletObject == null)
                    return;
                if(projectilePool != null)
                {               
                    bulletObject.transform.SetPositionAndRotation(_rb.position + _projectileSpawnModifier, Quaternion.identity);

                    //Make the projectile move in the desired direction.
                    _projectileSpawnModifier.Normalize();
                    bulletObject.GetComponent<Rigidbody2D>().AddForce(_projectileSpawnModifier * _bulletSpeed);

                    bulletObject.Deactivate();
                }
            
                //Handle the firing rate.
                _fireRate = Time.time + 0.07f;
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

    // invoked when creating an item to populate the object pool
    private ProjectileController CreateProjectile()
    {
        ProjectileController projectileInstance = Instantiate(defaultProjectile).GetComponent<ProjectileController>();
        projectileInstance.ObjectPool = projectilePool;
        return projectileInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(ProjectileController pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(ProjectileController pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(ProjectileController pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}
