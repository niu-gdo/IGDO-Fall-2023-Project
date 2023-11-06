using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemHandler : MonoBehaviour
{

    [SerializeField] private Weapon _currentWeapon;

    [SerializeField, Tooltip("Child Gameobject of Player that shows currentWeapon's sprite")]
    private GameObject _weaponView;


    public void UpdateItemView()
    {
        if (_weaponView == null)
        {
            return;
        }

        Vector3 pos = _currentWeapon._localViewPosition;

        if (GetComponent<CharacterController>().Direction == FacingDirection.Right)
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

        // create player sprite of wepaon
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
        switch (GetComponent<CharacterController>().Direction)
        {
            case FacingDirection.Right: dropDirection.x += 1; break;
            case FacingDirection.Left: dropDirection.x -= 1; break;
        }

        PickupObject.CreatePickup(dropDirection,_currentWeapon);

        //remove weapon from being equiped
        _currentWeapon = null;

        //remove player sprite of weapon
        Destroy(_weaponView);

    }
}
