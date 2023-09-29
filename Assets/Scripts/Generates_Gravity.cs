using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generates_Gravity : MonoBehaviour
{
    [SerializeField] private float _gravStrength = 20;
    private Generates_Gravity _self;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // GameObject temp = other.gameObject;
        if (other.TryGetComponent<Applies_Gravity>(out Applies_Gravity thing))
        {
            var temp = this;
            thing.Set(ref temp);
        }
        
    }

    public Vector3 CalcGrav(Rigidbody2D body)
    {
        return Vector3.down * _gravStrength * body.mass;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Applies_Gravity>(out Applies_Gravity thing))
        {
            thing.Reset(_self);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _self = this;
    }
}
