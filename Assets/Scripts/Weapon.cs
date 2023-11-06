using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [SerializeField] private string _description;
    //sprite of weapon in world and on player
    public Sprite _worldSprite, _playerSprite;
    //damage for projectile
    [SerializeField] private int _damage;
    //spawn point relative to player to spawn projectile
    [SerializeField] public Vector2 _localProjectilePosition,_localViewPosition;
    public abstract void Use();

}
