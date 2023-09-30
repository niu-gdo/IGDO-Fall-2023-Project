using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class Generates_Gravity : MonoBehaviour
{
    private Generates_Gravity _self;
    [SerializeField] private float _gravStrength = 20;
    [SerializeField] private int _gravType = 0;
    [SerializeField] private int _gravLevel = 0;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // GameObject temp = other.gameObject;
        if (other.TryGetComponent<Applies_Gravity>(out Applies_Gravity thing))
        {
            var temp = this;
            thing.Set(ref temp);
        }
        
    }

    public GravVec CalcGrav(Rigidbody2D body)
    {
        GravVec result;
        switch (_gravType)
        {
            case 0:  // Default down gravaty
                result = new GravVec(Vector2.down * _gravStrength * body.mass, _gravLevel);
                break;
            case 1:  // based on 
                var offset = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - body.position;
                var dist = offset.sqrMagnitude;
                var force = _gravStrength * body.mass / dist;
                var angle = offset.y / offset.x;
                var forceVec = new Vector2(math.cos(angle) * force, math.sin(angle) * force);

                result = new GravVec(forceVec, _gravLevel);
                break;
            default:
                result = new GravVec(Vector2.zero, _gravLevel);
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
