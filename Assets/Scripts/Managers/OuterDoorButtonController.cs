using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OuterDoorButtonController : MonoBehaviour
{
    public bool isPressed = false;
    private bool _oppositeOfisPressed;
    [SerializeField] private GameObject[] _connectedDoors;
    private void Awake()
    {
        // if isPressed is true opposite is set to false
        if(isPressed)
        {
            _oppositeOfisPressed = false;
        }
        // if iPressed is false opposite is set to true
        else
        {
            _oppositeOfisPressed = true;
        }
    }

    private void FixedUpdate()
    {
        // if isPressed becomes true 
        if (isPressed && _oppositeOfisPressed)
        {
            WhenisPressedChanges();
            _oppositeOfisPressed = false;
        }
        // if isPressed becomes false
        else if (!isPressed && !_oppositeOfisPressed)
        {
            WhenisPressedChanges();
            _oppositeOfisPressed = true;
        }
    }

    private void WhenisPressedChanges()
    {
        foreach (GameObject door in _connectedDoors)
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
