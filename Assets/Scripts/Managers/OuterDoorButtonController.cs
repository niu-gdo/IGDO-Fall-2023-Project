using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OuterDoorButtonController : MonoBehaviour
{
    [SerializeField] private bool isPressed = false;
    private bool oppositeOfIsPressed;
    [SerializeField] private GameObject[] connectedDoors;
    private void Awake()
    {
        // if isPressed is true opposite is set to false
        if(isPressed)
        {
            oppositeOfIsPressed = false;
        }
        // if iPressed is false opposite is set to true
        else
        {
            oppositeOfIsPressed = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // if isPressed becomes true 
        if (isPressed && oppositeOfIsPressed)
        {
            WhenIsPressedChanges();
            oppositeOfIsPressed = false;
        }
        // if isPressed becomes false
        else if (!isPressed && !oppositeOfIsPressed)
        {
            WhenIsPressedChanges();
            oppositeOfIsPressed = true;
        }
    }

    private void WhenIsPressedChanges()
    {
        foreach (GameObject door in connectedDoors)
        {
            // gets the isDoorOpen bool variable from each door and changes its value to the opposite of what it currently is
            bool isDoorOpen = door.GetComponent<DoorMovement>().isDoorOpen;
            if (isDoorOpen == false)
            {
                door.GetComponent<DoorMovement>().isDoorOpen = true;
            }
            else
            {
                door.GetComponent<DoorMovement>().isDoorOpen = false;
            }
        }
    }
    
}
