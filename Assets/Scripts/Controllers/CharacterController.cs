using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;

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
        _rb = GetComponent<Rigidbody2D>();
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
