using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;

    [SerializeField] private float _movementSpeed = 500f;
    [SerializeField] private float _jumpForce = 450f;

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private GameObject _weaponView;

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
            transform.localScale = new(1, 1);
        }else if(value.Get<Vector2>() == Vector2.left)
        {
            transform.localScale = new(-1, 1);
        }
    }

    public void OnJump(InputValue value)
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    public void OnInteract(InputValue value)
    {
        // Use a raycast to check if a pickup is in front of the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,.5f);

        //if pickup is in front of player, Pickup the wepon
        if (hit.collider != null 
            && hit.collider.GetComponent<PickupObject>() != null)
        {
            PickupWeapon(hit.collider.gameObject.GetComponent<PickupObject>());
        }
    }

    public void OnDrop(InputValue value)
    {
        //make weapon pickup in world
        MakeWeaponPickup();

        //remove weapon from being equiped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_weaponView);

    }

    public void PickupWeapon(PickupObject obj)
    {
        //equip weapon in player
        _currentWeapon = obj.wepaon;
        // create player sprite of wepaon
        _weaponView = new GameObject(
            _currentWeapon.name,
            typeof(SpriteRenderer)
        );
        //set player weapon to my a child, and display
        _weaponView.transform.SetParent(this.transform);
        _weaponView.transform.localPosition = new Vector2(0.7f, .1f);
        _weaponView.GetComponent<SpriteRenderer>().sprite = _currentWeapon._playerSprite;

        //remove world object
        Destroy(obj.gameObject);
    }

    private void MakeWeaponPickup()
    {
        //create GameObject
        GameObject pickup = new GameObject(
            _currentWeapon.name,
            typeof(BoxCollider2D),
            typeof(SpriteRenderer),
            typeof(PickupObject)
        );
        //set position in world
        pickup.transform.position = new Vector2(
            transform.position.x + 1,
            transform.position.y
         );

        //setup function stuffs
        pickup.GetComponent<PickupObject>().wepaon = _currentWeapon;
        pickup.GetComponent<BoxCollider2D>().isTrigger = true;
        pickup.GetComponent<SpriteRenderer>().sprite = _currentWeapon._Worldsprite;
    }

}
