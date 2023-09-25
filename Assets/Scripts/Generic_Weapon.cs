using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Generic",menuName = "Weapon/Generic")]
public class Generic_Weapon : Weapon
{
    public override void Use()
    {
        Debug.Log("POW!");
    }
}
