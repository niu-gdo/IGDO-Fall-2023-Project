using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// What method to use to calculate net gravity forces
/// </summary>
public enum GravLevelUse
{
    Sum,        // Just sum all applied forces
    Highest     // Sum all forces with the highest given priority
}

/// <summary>
/// Any entity that has this receiver can register hooks from Generators to ask for gravity calculations.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class AppliesGravity : MonoBehaviour
{
    // Keeps track of all gravity fields that we are currently touching
    private List<GeneratesGravity> _appliedForces;
    [SerializeField] private GravLevelUse _gravLevelUse = GravLevelUse.Sum;
    
    private Rigidbody2D _rb;   

    /// <summary>
    /// Try to insert Generator reference into list of forces
    /// </summary>
    /// <param name="grav">Reference of the Generator connected to the gravity field collider</param>
    public void Set(ref GeneratesGravity grav)
    // Insert interacting field to list if it's new (Avoids double counting n stuff)
    {
        // Make sure it is new
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
    }

    /// <summary>
    /// Remove Generator when out of range (detected by exiting collider)
    /// </summary>
    /// <param name="grav">Reference of Generator to be removed</param>
    public void Reset(GeneratesGravity grav)
    // Removes Gravity field from tracking list
    {
        _appliedForces.RemoveAll(i => i.GetInstanceID() == grav.GetInstanceID());
    }
    
    
    /// <summary>
    /// Load our rigidbody which is needed to apply forces to
    /// </summary>
    void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _appliedForces = new List<GeneratesGravity>();
    }

    /// <summary>
    /// Applies the gravity forces on each step
    /// </summary>
    void FixedUpdate()
    {
        // Check how to interpret the level
        switch (_gravLevelUse)
        {
            case GravLevelUse.Sum:  // Just add up all forces we calculate
                foreach (var force in _appliedForces) {
                    _rb.AddForce(force.CalcGrav(_rb).Force);
                }
                break;
            case GravLevelUse.Highest:  // Sum up all forces of current highest level
                var highest = -999;
                var vecSum = Vector2.zero;
                foreach (var force in _appliedForces)
                {
                    var res = force.CalcGrav(_rb);
                    if (res.Level > highest)  // reset sum if new highest found
                    {
                        highest = res.Level;
                        vecSum = res.Force;
                    }
                    else if (res.Level == highest)
                    {
                        vecSum += res.Force;
                    }
                }
                _rb.AddForce(vecSum);
                break;
            default:
                break;
        }
        
    }
}
