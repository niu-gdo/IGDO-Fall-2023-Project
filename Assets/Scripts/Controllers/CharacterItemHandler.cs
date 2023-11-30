using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemHandler : MonoBehaviour
{

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField, Tooltip("Child Gameobject of Player that shows currentWeapon's sprite or gameobject")]
    private GameObject _itemView;
    [SerializeField] private bool   _isHoldingItem;
    [SerializeField] private Vector2 _localItemPosition;

    private FixedJoint2D _joint
    {
        get { return GetComponent<FixedJoint2D>(); }
    }

    public void UpdateItemView()
    {
        Vector2 position;

        if (_itemView == null)
        {
            return;
        }

        position = (_currentWeapon != null) ? _currentWeapon._localViewPosition : _localItemPosition;

        if (GetComponent<CharacterController>().Direction == FacingDirection.Right)
        {
            _itemView.transform.localPosition = position;
            _itemView.transform.localScale = new(1, 1);

        }
        else
        {
            _itemView.transform.localPosition = new(position.x * -1, position.y, 0);
            _itemView.transform.localScale = new(-1, 1);
        }
    }

    public void PickupWeapons(Weapon weapon)
    {
        _isHoldingItem = true;

        //equip weapon in player
        _currentWeapon = weapon;

        // create player sprite of wepaon
        _itemView = new GameObject(
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

        _joint.connectedBody = _itemView.GetComponent<Rigidbody2D>();

        _joint.enabled = true;

        UpdateItemView();
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
        switch (GetComponent<CharacterController>().Direction)
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

        //remove weapon from being equiped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_itemView);
    }

    private void DropOther()
    {
        _joint.connectedBody = null;

        _joint.enabled = false;

        //parent _itemView
        _itemView.transform.SetParent(null);
    }

    private void OnJointBreak2D(Joint2D joint)
    {
        DropOther();
    }
}
