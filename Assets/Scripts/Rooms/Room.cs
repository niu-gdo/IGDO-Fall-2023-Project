using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A playable room instance.
/// </summary>
public class Room : MonoBehaviour
{
    [SerializeField] private string _id;

    // Read-only accessor
    public string Id { get => _id; }
    private readonly Dictionary<string, TransitionEndpoint> _roomEndpoints = new Dictionary<string, TransitionEndpoint>();

    /// <summary>
    /// Attempt to find an endpoint in this room by a given ID.
    /// </summary>
    /// <param name="endpointId">Id of the endpoint to query for.</param>
    /// <returns>The target endpoint, or NULL if not found.</returns>
    public TransitionEndpoint FindEndpoint(string endpointId)
    {
        if (_roomEndpoints.TryGetValue(endpointId, out TransitionEndpoint targetEndpoint))
            return targetEndpoint;

        return null;
    }

    /// <summary>
    /// Prepare this room for being played in.
    /// </summary>
    public void LoadRoom()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Remove the room from play and set to inactive.
    /// </summary>
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
            _roomEndpoints.Add(endPoint.Id, endPoint);
        }
    }
}
