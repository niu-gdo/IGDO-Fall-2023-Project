using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemHandler : MonoBehaviour
{

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private bool   _isHoldingItem;
    [SerializeField] private Vector3 _localItemPosition;

    [SerializeField, Tooltip("Child Gameobject of Player that shows currentWeapon's sprite or gameobject")]
    private GameObject _itemView;


    public void UpdateItemView(Vector3 pos)
    {
        if (_itemView == null)
        {
            return;
        }

        if (GetComponent<CharacterController>().Direction == FacingDirection.Right)
        {
            _itemView.transform.localPosition = pos;
            _itemView.transform.localScale = new(1, 1);

        }
        else
        {
            _itemView.transform.localPosition = new(pos.x * -1, pos.y, 0);
            _itemView.transform.localScale = new(-1, 1);
        }
    }

    public void PickupWeapon(Weapon weapon)
    {
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

        UpdateItemView(_currentWeapon._localViewPosition);
    }

    public void PickupOther(GameObject obj)
    {
        //set gameobject to _itemView
        _itemView = obj;

        //parent _itemView
        _itemView.transform.SetParent(this.transform);

        UpdateItemView(_localItemPosition);
    }

    public void DropItem()
    {
        Vector2 dropDirection = transform.position;
        
        //get position in world to spawn pickup
        switch (GetComponent<CharacterController>().Direction)
        {
            case FacingDirection.Right: dropDirection.x += 1; break;
            case FacingDirection.Left: dropDirection.x -= 1; break;
        }

        PickupObject.CreatePickup(dropDirection,_currentWeapon);

        //remove weapon from being equiped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_itemView);

    }
}
