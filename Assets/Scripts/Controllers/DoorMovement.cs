using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorMovement : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private float movementSpeed = 1f;
    public bool isDoorOpen = false;
    private Vector2 closedPosition;
    private Vector2 openPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        closedPosition = transform.position;
    }
    void Start()
    {
        openPosition = transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDoorOpen && (Vector2)transform.position != openPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, openPosition, Time.fixedDeltaTime * movementSpeed);
        }
        if(!isDoorOpen && (Vector2)transform.position != closedPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, closedPosition, Time.fixedDeltaTime * movementSpeed);
        }
    }
}
