using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

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
    [SerializeField] private float _pickupRange = .5f;

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private GameObject _weaponView;

    private enum FacingDirection{Right,Left}

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
        
        if(_weaponView != null) {UpdateWeaponView();}
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    public void OnInteract(InputValue value)
    {
        Vector2 rayDir;
        if(_direction == FacingDirection.Right)
        {
            rayDir = Vector2.right;
        }else{
            rayDir = Vector2.left;
        }
        // Use a raycast to check if a pickup is in front of the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir,_pickupRange);
        Debug.Log(hit.collider);
        //if pickup is in front of player, Pickup the wepon
        if (hit.collider != null 
            && hit.collider.GetComponent<PickupObject>() != null)
        {
            PickupWeapon(hit.collider.gameObject.GetComponent<PickupObject>());
        }
    }

    public void OnDrop(InputValue value)
    {
        //drop pickup object in world
        DropItem();

        //remove weapon from being equiped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_weaponView);

    }

    public void PickupWeapon(PickupObject obj)
    {
        //equip weapon in player
        _currentWeapon = obj._weapon;

        // create player sprite of wepaon
        _weaponView = new GameObject(
            _currentWeapon.name,
            typeof(SpriteRenderer)
        );

        //set weaponView as a child of player, and display sprite
        _weaponView.transform.SetParent(this.transform);
        _weaponView.GetComponent<SpriteRenderer>().sprite = _currentWeapon._playerSprite;

        UpdateWeaponView();
        //remove world object
        Destroy(obj.gameObject);
    }

    private void DropItem()
    {
        Vector2 dropDirection = transform.position;

        //create GameObject
        GameObject pickup = new GameObject(
            _currentWeapon.name,
            typeof(BoxCollider2D),
            typeof(SpriteRenderer),
            typeof(PickupObject)
        );
        //set position in world
        switch(_direction)
        {
            case FacingDirection.Right: dropDirection.x += 1; break;
            case FacingDirection.Left: dropDirection.x -= 1; break;
        }

        pickup.transform.position = dropDirection;

        //setup function stuffs
        pickup.GetComponent<PickupObject>().InitlizeObject(_currentWeapon);
    }

    private void UpdateWeaponView()
    {    
        Vector3 pos = _currentWeapon._localViewPosition;

        if(_direction == FacingDirection.Right)
        {   
            _weaponView.transform.localPosition = pos;
            _weaponView.transform.localScale = new(1,1);
            
        }else{
            _weaponView.transform.localPosition = new(pos.x*-1,pos.y,0);
            _weaponView.transform.localScale = new(-1,1);
        }
    }
}
