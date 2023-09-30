using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public enum GravLevelUse
{
    Sum,
    Highest
}

public class Applies_Gravity : MonoBehaviour
{
    
    private Rigidbody2D _rb;   
    // Keeps track of all gravity fields that we are currently touching
    private List<Generates_Gravity> _appliedForces = new List<Generates_Gravity>();
    [SerializeField] private GravLevelUse _gravLevelUse = GravLevelUse.Sum;

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

        switch (_gravLevelUse)
        {
            case GravLevelUse.Sum:
                foreach (var force in _appliedForces)
                {
                    _rb.AddForce(force.CalcGrav(_rb).Force);
                }
                break;
            case GravLevelUse.Highest:
                var highest = -999;
                var vecSum = Vector2.zero;
                foreach (var force in _appliedForces)
                {
                    var res = force.CalcGrav(_rb);
                    if (res.Level > highest)
                    {
                        highest = res.Level;
                        vecSum = res.Force;
                    }
                    else if (res.Level == highest)
                    {
                        vecSum += res.Force;
                    }
                }
                break;
            default:
                break;
        }
        
    }
}
