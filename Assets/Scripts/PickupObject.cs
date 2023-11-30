using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupObject : MonoBehaviour, IInteractible
{
    public void Interaction(CharacterController c)
    {

        c._characterItemHandler.PickupObject(this.gameObject);
    }
}
