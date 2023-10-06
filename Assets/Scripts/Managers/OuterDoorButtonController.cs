using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OuterDoorButtonController : MonoBehaviour
{
    public bool isPressed = false;
    // below code checks for when the variable isPressed changes, this can be used to connect different actions to the button
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



    [SerializeField] private GameObject[] connectedDoors;
    private void Awake()
    {
        // add this code to any objects that need to subscribe and listen to event
        // componentWithEvent.OnVariableChange += VariableChangeHandler;
        OnVariableChange += VariableChangeHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject door in connectedDoors)
        {
            bool[] doorParts = door.GetComponents<bool>();
            for(int i = 0; i < doorParts.Length; i++)
            {
                Debug.Log(doorParts[i]); // try to print list of bool variable names
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // this is the code that runs whenever the variable changes
    private void VariableChangeHandler(bool newVal)
    {
        foreach (GameObject door in connectedDoors)
        {
            door.GetComponent<bool>();
        }
    }
}
