using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class projectileController : MonoBehaviour
{
    public CircleCollider2D collisionTracker;
    public Vector2 bulletDirection;
    public float bulletSpeed;
    
    
    void Awake ()
    {
        
    }
    
    
    void Update ()
    {
        Shoot();
    }
    
    
    void Shoot ()
    {
        
    }
}
