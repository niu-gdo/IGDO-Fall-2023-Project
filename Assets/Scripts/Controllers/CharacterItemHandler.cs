using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterItemHandler : MonoBehaviour
{

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField, Tooltip("Child Gameobject of Player that shows currentWeapon's sprite or gameobject")]
    private GameObject _itemView;
    [SerializeField] private bool   _isHoldingItem;
    [SerializeField] private Vector2 _localItemPosition;

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
        Vector2 position;

        if (_itemView == null)
        {
            return;
        }

        position = (_currentWeapon != null) ? _currentWeapon._localViewPosition : _localItemPosition;

        if (Direction == FacingDirection.Right)
        {
            _itemView.transform.localPosition = position;

            _itemView.transform.localScale = new(Mathf.Abs(_itemView.transform.localScale.x), _itemView.transform.localScale.y);

        }
        else
        {
            _itemView.transform.localPosition = new(position.x * -1, position.y, 0);

            _itemView.transform.localScale = new(Mathf.Abs(_itemView.transform.localScale.x)*-1, _itemView.transform.localScale.y);
        }
    }

    public void PickupWeapons(Weapon weapon)
    {
        _isHoldingItem = true;

        //equip weapon in player
        _currentWeapon = weapon;

        // create player sprite of weapon
        _weaponView = new GameObject(
            _currentWeapon.name,
            typeof(SpriteRenderer)
        );

        //set weaponView as a child of player, and display sprite
        _itemView.transform.SetParent(this.transform);
        _itemView.GetComponent<SpriteRenderer>().sprite = _currentWeapon._playerSprite;

        UpdateItemView();
    }

    public void PickupObject(GameObject obj)
    {
        _isHoldingItem = true;

        //set gameobject to _itemView
        _itemView = obj;

        //parent _itemView
        _itemView.transform.SetParent(this.transform);
        Destroy(_itemView.GetComponent<AppliesGravity>());
        Destroy(_itemView.GetComponent<Rigidbody2D>());

        UpdateItemView();

        if(TryGetComponent<CharacterMovement>(out CharacterMovement move))
        {
            move.ChangeJumpMod(-JumpPenalty);
            move.ChangeSpeedMod(-250);
        }
    }

    public void DropItem()
    {
        if(!_isHoldingItem)
        {
            return;
        }

        _isHoldingItem = false;

        Vector2 dropDirection = transform.position;
        
        //get position in world to spawn pickup
        switch (Direction)
        {
            case FacingDirection.Right: dropDirection.x += 1; break;
            case FacingDirection.Left: dropDirection.x -= 1; break;
        }

        if(_currentWeapon != null)
        {
            DropWeapon(dropDirection);
        }
        else
        {
            DropOther();
        }

    }

    private void DropWeapon(Vector2 direction)
    {
        PickupWeapon.CreatePickup(direction, _currentWeapon);

        //remove weapon from being equipped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_weaponView);

    }

    public void OnMove(InputValue value) => ProcessMovement(value);
   
}
