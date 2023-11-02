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
        InitializeEndpointDict();
    }

    private void InitializeEndpointDict()
    {
        TransitionEndpoint[] endpoints = GetComponentsInChildren<TransitionEndpoint>();

        foreach (TransitionEndpoint endPoint in endpoints)
        {
            roomEndpoints.Add(endPoint.Id, endPoint);
        }
    }
}
