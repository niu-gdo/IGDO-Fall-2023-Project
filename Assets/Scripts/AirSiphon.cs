using UnityEngine;

public class AirSiphon : MonoBehaviour
{

    [SerializeField] private string _sideA;
    [SerializeField] private string _sideB;
    [SerializeField] private bool _open = false;
    [SerializeField, Range(0, 1)] private float _rate;  // Based on proportion of relative difference between sides

    // Update is called once per frame
    void FixedUpdate()
    {
        // Make sure both sides of the siphon exist
        if (AirZoneManager.AirLevels.ContainsKey(_sideA) && AirZoneManager.AirLevels.ContainsKey(_sideB) && _open)
        {
            // Temps to clean up long calls a bit
            var tempA = AirZoneManager.AirLevels[_sideA];
            var tempB = AirZoneManager.AirLevels[_sideB];
            
            // 1 represents full correction instantly
            if (_rate == 1)
            {
                if (tempA.Mode == ZoneMode.Dynamic && tempB.Mode == ZoneMode.Dynamic)
                {
                    var newVal = (tempA.AirLevel + tempB.AirLevel) / 2;
                    AirZoneManager.AirLevels[_sideA].AirLevel = newVal;
                    AirZoneManager.AirLevels[_sideB].AirLevel = newVal;
                } else if (tempA.Mode == ZoneMode.Constant && tempB.Mode == ZoneMode.Dynamic)
                {
                    AirZoneManager.AirLevels[_sideB].AirLevel = tempA.AirLevel;
                } else if (tempA.Mode == ZoneMode.Dynamic && tempB.Mode == ZoneMode.Constant)
                {
                    AirZoneManager.AirLevels[_sideA].AirLevel = tempB.AirLevel;
                }
            }
            else  // Normal exchange
            {
                var diff = AirZoneManager.AirLevels[_sideA].AirLevel - AirZoneManager.AirLevels[_sideB].AirLevel;
                // Only change the target zone if it's set to dynamic
                if (tempA.Mode == ZoneMode.Dynamic)
                {
                    AirZoneManager.AirLevels[_sideA].AirLevel -= _rate * diff / 2;
                }
                if (tempB.Mode == ZoneMode.Dynamic)
                {
                    AirZoneManager.AirLevels[_sideB].AirLevel += _rate * diff / 2;
                }
            }
        }
        else
        {
            if (_open)  // If we enter here, that means there dict issues
            {
                var text = "One of the siphons is missing:\n";
                text += _sideA + " = " + AirZoneManager.AirLevels.ContainsKey(_sideA) + "\n";
                text += _sideB + " = " + AirZoneManager.AirLevels.ContainsKey(_sideB);
                Debug.Log(text);
            }
        }
    }
}
