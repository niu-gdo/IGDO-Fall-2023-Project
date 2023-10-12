using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OuterDoorButtonController : MonoBehaviour
{
    [SerializeField] private bool isPressed = false;
    // below code checks for when the variable isPressed changes, this can be used to connect different actions to the button
    /*
    public bool isPressedChecker
    {
        get { return isPressed; }
        set
        {
            if (isPressed == value) return;
            isPressed = value;
            if (OnVariableChange != null)
                OnVariableChange(isPressed);
        }
    }
    public delegate void OnVariableChangeDelegate(bool newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    */

    public bool isPressedChecker
    {
        get { return isPressed; }
        set
        {
            if (isPressed != value)
            {
                isPressed = value;
                Debug.Log("working?");
                // Run some function or event here
                
            }
        }
    }
    public delegate void OnVariableChangeDelegate(bool newVal);
    public event OnVariableChangeDelegate OnVariableChange;


    [SerializeField] private GameObject[] connectedDoors;
    private void Awake()
    {
        // add the below code to any objects that need to subscribe and listen to event
        // componentWithEvent.OnVariableChange += VariableChangeHandler;
        //this.OnVariableChange += VariableChangeHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        //isPressedChecker = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // this is the code that runs whenever the variable changes
    /*
    private void VariableChangeHandler(bool newVal)
    {
        foreach (GameObject door in connectedDoors)
        {
            Debug.Log("Is this working?" + door.GetComponent<DoorMovement>().isDoorOpen);
            // gets the isDoorOpen bool variable from each door and changes its value to the opposite of what it currently is
            bool isDoorOpen = door.GetComponent<DoorMovement>().isDoorOpen;
            if(isDoorOpen == false)
            {
                door.GetComponent<DoorMovement>().isDoorOpen = true;
            }
            else
            {
                door.GetComponent<DoorMovement>().isDoorOpen = false;
            }
        }
    }
    */
}
