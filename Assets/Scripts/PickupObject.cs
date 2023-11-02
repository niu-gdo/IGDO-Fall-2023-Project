using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupObject : MonoBehaviour
{
    public Weapon _weapon;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        if(_weapon != null)
        {
            GetComponent<SpriteRenderer>().sprite = _weapon._Worldsprite;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void InitlizeObject(Weapon weapon)
    {
        _weapon = weapon;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().sprite = weapon._Worldsprite;
    }
    
}
