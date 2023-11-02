using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string Id = "";
    public Dictionary<string, TransitionEndpoint> roomEndpoints = new Dictionary<string, TransitionEndpoint>();


    public void LoadRoom()
    {
        gameObject.SetActive(true);
    }

    public void UnloadRoom()
    {
        gameObject.SetActive(false);
    }


    private void Awake()
    {
        TransitionEndpoint[] endpoints = GetComponentsInChildren<TransitionEndpoint>();

        foreach (TransitionEndpoint endPoint in endpoints)
        {
            Debug.Log($"Endpoint ID: {endPoint.Id}");
            roomEndpoints.Add(endPoint.Id, endPoint);
        }
    }

}
