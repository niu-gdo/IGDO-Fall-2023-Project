using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// An enum of options on what calculation to use find the gravity force
/// </summary>
public enum GravTypes
{
    Linear,  // Make it go down
    Realistic  // Pull to center
}

/// <summary>
/// Any entity with this component uses it's attached collider2d to hook itself into gravity appliers to provide a gravity influence
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class GeneratesGravity : MonoBehaviour
{
    [SerializeField] private float _gravStrength = 20;  // The strength of the gravity force
    [SerializeField] private GravTypes _gravType = GravTypes.Linear;  // What mode is used to calculate gravity force
    [SerializeField] private float _gravAngle = 3 * math.PI / 2;  // Change the angle of the linear gravity force (allows angled gravity)
    [SerializeField] public int _gravPriority = 0;  // How important the field is (Applier has a setting to ignore lower levels)
    [SerializeField] private float _maxForce = 30;  // To stop insanely high forces when getting close to origin
    private GeneratesGravity _self;  // Generating a mutable reference to self for passing
    
    /// <summary>
    /// Try to insert this collider into Applier if other has component
    /// </summary>
    /// <param name="other">The other entity that interacted with our collider</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // GameObject temp = other.gameObject;
        if (other.TryGetComponent<AppliesGravity>(out AppliesGravity thing))
        {
            thing.Set(ref _self);
        }
        
    }

    /// <summary>
    /// Remove self from the other entity when it leaves our collider range
    /// </summary>
    /// <param name="other">The other entity that interacted with our collider</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<AppliesGravity>(out AppliesGravity thing))
        {
            thing.Reset(_self);
        }
    }

    /// <summary>
    /// Uses known self information and passed rigidbody information to calculate the gravity force that will be applied to the other body
    /// </summary>
    /// <param name="body">The entity that is requesting a gravity force calculation</param>
    /// <returns>The amount of force applied from this field. Also includes the force priority</returns>
    public GravVec CalcGrav(Rigidbody2D body)
    {
        GravVec result;
        switch (_gravType)
        {
            case GravTypes.Linear:  // Gravity simply forces in a single direction regardless of location
                var linForce = _gravStrength * body.mass;
                // Sin and cos are used to split force into x and y components
                var linVec = new Vector2(linForce * math.cos(_gravAngle), linForce * math.sin(_gravAngle));
                result = new GravVec(linVec, _gravPriority);
                break;
            case GravTypes.Realistic:  // Newton gravity. Higher forces to center of mass
                var offset = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - body.position;
                var dist = (float)math.max(offset.sqrMagnitude, .001);  // Cap closest distance to avoid insanely high force/div by 0
                var force = math.min(_gravStrength * body.mass / dist, _maxForce);
                //  https://math.stackexchange.com/questions/1327253/how-do-we-find-out-angle-from-x-y-coordinates
                var denom = (float)math.max(math.sqrt(math.pow(offset.x, 2) + math.pow(offset.y, 2)), .000000001);  // Max because I want to avoid div by 0
                var angleX = offset.x / denom;
                var angleY = offset.y / denom;
                var forceVec = new Vector2(angleX * force, angleY * force);

                result = new GravVec(forceVec, _gravPriority);
                break;
            default:
                result = new GravVec(Vector2.zero, _gravPriority );
                break;
        }

        return result;
    }

     /// <summary>
     /// Field just needs to grab a ref of itself for hooking purposes.
     /// </summary>
    void Start()
    {
        _self = this;
    }
}
