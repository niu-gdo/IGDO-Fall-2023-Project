using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterItemHandler : MonoBehaviour
{

    [SerializeField] private Weapon _currentWeapon;

    [SerializeField, Tooltip("Child Gameobject of Player that shows currentWeapon's sprite")]
    private GameObject _weaponView;

    [SerializeField] private float _pickupRange = .5f;

    private FacingDirection _direction = FacingDirection.Right;

    public FacingDirection Direction{ get => _direction; }

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
            interactible.Interaction(GetComponent<CharacterController>());
        }
    }

    public void OnDrop(InputValue value)
    {
        DropItem();

    }


    private void ProcessMovement(InputValue value)
    {
        var _movement = value.Get<Vector2>();

        if (value.Get<Vector2>() == Vector2.right)
        {
            _direction = FacingDirection.Right;
        }
        else if (value.Get<Vector2>() == Vector2.left)
        {
            _direction = FacingDirection.Left;
        }

        UpdateItemView();
    }


    public void UpdateItemView()
    {
        if (_weaponView == null)
        {
            return;
        }

        Vector3 pos = _currentWeapon._localViewPosition;

        if (Direction == FacingDirection.Right)
        {
            _weaponView.transform.localPosition = pos;
            _weaponView.transform.localScale = new(1, 1);

        }
        else
        {
            _weaponView.transform.localPosition = new(pos.x * -1, pos.y, 0);
            _weaponView.transform.localScale = new(-1, 1);
        }
    }

    public void PickupWeapon(Weapon weapon)
    {
        //equip weapon in player
        _currentWeapon = weapon;

        // create player sprite of weapon
        _weaponView = new GameObject(
            _currentWeapon.name,
            typeof(SpriteRenderer)
        );

        //set weaponView as a child of player, and display sprite
        _weaponView.transform.SetParent(this.transform);
        _weaponView.GetComponent<SpriteRenderer>().sprite = _currentWeapon._playerSprite;

        UpdateItemView();
    }


    public void DropItem()
    {
        Vector2 dropDirection = transform.position;
        
        //get position in world to spawn pickup
        switch (Direction)
        {
            case FacingDirection.Right: dropDirection.x += 1; break;
            case FacingDirection.Left: dropDirection.x -= 1; break;
        }

        PickupObject.CreatePickup(dropDirection,_currentWeapon);

        //remove weapon from being equipped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_weaponView);

    }

    public void OnMove(InputValue value) => ProcessMovement(value);
   
}
