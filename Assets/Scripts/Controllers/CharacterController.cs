using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

/// <summary>
/// Processes and propagates player input.
/// </summary>
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
    [SerializeField] private float _pickupRange = .5f;

    [SerializeField] private FacingDirection _direction;

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
