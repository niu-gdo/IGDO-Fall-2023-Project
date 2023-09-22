using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class projectileController : MonoBehaviour
{
    private Rigidbody2D _projectileCollision;
    private Rigidbody2D _playerPhysicsBody;
    private Vector2 _bulletDirection;
    private float _bulletSpeed = 800f;
    private Camera _cam;
    private GameObject _playerCharacter;
    
    void Awake ()
    {
        //Get position and physics components of projectile; initialize camera variable for converting world to screen coordinates.
        _projectileCollision = GetComponent<Rigidbody2D>();
        _cam = Camera.main;

        //Get velocity of player character.
        _playerCharacter = GameObject.Find("Player");
        _playerPhysicsBody = _playerCharacter.GetComponent<Rigidbody2D>();

        //Get the direction of the velocity vector of the bullet.
        _bulletDirection = Input.mousePosition - _cam.WorldToScreenPoint(_projectileCollision.position);
        _bulletDirection.Normalize();

        _projectileCollision.AddForce(_bulletDirection * _bulletSpeed + _playerPhysicsBody.velocity);
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
