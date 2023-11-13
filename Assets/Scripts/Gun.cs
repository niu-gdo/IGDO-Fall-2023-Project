using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class Gun : Weapon
{
    public override void Use()
    {
        Debug.Log("POW");
    }
}
