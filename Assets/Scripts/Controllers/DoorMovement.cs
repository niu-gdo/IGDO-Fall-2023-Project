using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorMovement : MonoBehaviour
{
    private Vector2 movement;
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
        _openPosition = transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
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
