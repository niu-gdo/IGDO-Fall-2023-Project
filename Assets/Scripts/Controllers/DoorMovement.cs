using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    public bool isDoorOpen = false;
    private Vector2 _closedPosition;
    private Vector2 _openPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        _closedPosition = transform.position;
    }
    void Start()
    {
        // the **first** child of the door object will be the position that the door will move to when it is open. can change later to check for tags in case the open position can't be the first child object of the door
        _openPosition = transform.GetChild(0).transform.position;
    }

    private void FixedUpdate()
    {
        // checks to make sure door is in correct position based on "isDoorOpen" bool variable and current position. if it's not, smoothly moves door to correct position
        if (isDoorOpen && (Vector2)transform.position != _openPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _openPosition, Time.fixedDeltaTime * _movementSpeed);
        }
        if(!isDoorOpen && (Vector2)transform.position != _closedPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, _closedPosition, Time.fixedDeltaTime * _movementSpeed);
        }
    }
}
