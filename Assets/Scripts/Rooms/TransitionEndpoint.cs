using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A location in which the player can be moved to in order to enter the room.
/// </summary>
public class TransitionEndpoint : MonoBehaviour
{
    [SerializeField] private string _id = "";

    // Read only accessor
    public string Id { get => _id; }
}
