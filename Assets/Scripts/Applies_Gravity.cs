using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Applies_Gravity : MonoBehaviour
{
    
    private Rigidbody2D _rb;   
    // Keeps track of all gravity fields that we are currently touching
    private List<Generates_Gravity> _appliedForces = new List<Generates_Gravity>();

    public void Set(ref Generates_Gravity grav)
    // Insert interacting field to list if it's new (Avoids double counting n stuff)
    {
        var newSource = true;
        foreach (var item in _appliedForces)
        {
            if (item.GetInstanceID() == grav.GetInstanceID())
            {
                newSource = false;
            }
        }

        if (newSource)
        {
            _appliedForces.Add(grav);
        }
        Debug.Log("Added " + grav.GetInstanceID());
    }

    public void Reset(Generates_Gravity grav)
    // Removes Gravity field from tracking list
    {
        _appliedForces.RemoveAll(i => i.GetInstanceID() == grav.GetInstanceID());
        Debug.Log("Removed " + grav.GetInstanceID());
    }
    
    
    // Start is called before the first frame update
    void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Apply each force to the current body
        foreach (var force in _appliedForces)
        {
            _rb.AddForce(force.CalcGrav(_rb).Force);
        }
        
    }
}
