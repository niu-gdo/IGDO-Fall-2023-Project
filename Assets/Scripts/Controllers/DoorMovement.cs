using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private float movementSpeed = 100f;
    [SerializeField] private float maxDistance = 30f;
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
        if(gameObject.tag == "outerDoorTop")
        {
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        }
    }
}
