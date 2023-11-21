using UnityEngine;

public enum ZoneMode { // Can the air level change?
    Dynamic,
    Constant
}

public class AirZone : FloatingDebugText
{

    [SerializeField] private string _name;  
    public float AirLevel = 0;
    public ZoneMode Mode;
    
    // Start is called before the first frame update
    new void Start()
    {
        // Start the debugging text
        base.Start();
        
        // Add box into room singleton
        AirZoneManager.AirLevels.TryAdd(_name, this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Try to print debug text if needed
        UpdateText(_name + ": " + AirLevel);
       
    }
}
