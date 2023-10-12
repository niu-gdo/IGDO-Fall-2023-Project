using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private float movementSpeed = 100f;
    [SerializeField] private float maxDistance = 30f;
    public bool isDoorOpen = false;
    private Vector2 closedPosition;
    private Vector2 openPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        closedPosition = transform.position;
        openPosition = transform.GetChild(0).transform.position;
    }
    void Start()
    {
        //Debug.Log(gameObject.transform.GetChild(0).transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(isDoorOpen && (Vector2)transform.position != openPosition)
        {
            transform.position = Vector2.Lerp(transform.position, openPosition, Time.deltaTime * movementSpeed);
        }
        if(!isDoorOpen && (Vector2)transform.position != closedPosition)
        {
            transform.position = Vector2.Lerp(transform.position, closedPosition, Time.deltaTime * movementSpeed);
        }
    }
}
