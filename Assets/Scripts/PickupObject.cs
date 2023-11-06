using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupObject : MonoBehaviour, IInteractible
{
    public Weapon _weapon;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        if(_weapon != null)
        {
            GetComponent<SpriteRenderer>().sprite = _weapon._worldSprite;
        }else{
            gameObject.SetActive(false);
            Debug.LogWarning($"PickupObject::Start on Object {gameObject.name} - Weapon data is null, disabling self.");
        }
    }

    /// <summary>
    /// Programitcly create a Gameobject with type PickupObject
    /// </summary>
    /// <param name="spawnLocation">Where in the world to place the Gameobject</param>
    /// <param name="weapon">Weapon data to populate _weapon</param>
    public static void CreatePickup(Vector2 spawnLocation,Weapon weapon)
    {
        //create GameObject
        GameObject pickup = new GameObject(
            weapon.name, 
            typeof(BoxCollider2D),
            typeof(SpriteRenderer),
            typeof(PickupObject)
        );

        pickup.transform.position = spawnLocation;

        pickup.GetComponent<BoxCollider2D>().isTrigger = true;
        pickup.GetComponent<SpriteRenderer>().sprite = weapon._worldSprite;
        pickup.GetComponent<PickupObject>()._weapon = weapon;

        pickup.SetActive(true);
    }

    public void Interaction(CharacterController c)
    {

        c._characterItemHandler.PickupWeapon(_weapon);

        //remove self from world
        Destroy(this.gameObject);
    }
}
