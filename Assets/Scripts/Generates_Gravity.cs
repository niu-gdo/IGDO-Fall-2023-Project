using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public enum GravTypes
{
    Linear,
    Realistic
}

public class Generates_Gravity : MonoBehaviour
{
    private Generates_Gravity _self;
    [SerializeField] private float _gravStrength = 20;
    [SerializeField] private GravTypes _gravType = GravTypes.Linear;
    [SerializeField] private int _gravPriority = 0;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // GameObject temp = other.gameObject;
        if (other.TryGetComponent<Applies_Gravity>(out Applies_Gravity thing))
        {
            thing.Set(ref _self);
        }
        
    }

    public GravVec CalcGrav(Rigidbody2D body)
    {
        GravVec result;
        switch (_gravType)
        {
            case GravTypes.Linear:  // Default down gravaty
                result = new GravVec(Vector2.down * _gravStrength * body.mass, _gravPriority);
                break;
            case GravTypes.Realistic:  // based on 
                var offset = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - body.position;
                var dist = offset.sqrMagnitude;
                var force = _gravStrength * body.mass / dist;
                var angle = offset.y / offset.x;
                var forceVec = new Vector2(math.cos(angle) * force, math.sin(angle) * force);

                result = new GravVec(forceVec, _gravPriority);
                break;
            default:
                result = new GravVec(Vector2.zero, _gravPriority );
                break;
        }

        return result;
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
