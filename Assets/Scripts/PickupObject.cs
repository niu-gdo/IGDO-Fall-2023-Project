using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupObject : MonoBehaviour, IInteractible
{
    public void Interaction(CharacterItemHandler c)
    {

        c.PickupObject(this.gameObject);
    }
}
