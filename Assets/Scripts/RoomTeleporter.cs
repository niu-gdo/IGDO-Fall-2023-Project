using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomTeleporter : MonoBehaviour
{
    private LevelManager _levelManager;

    public string _destinationRoomId = "";
    public string _destinationEndpointId = "";

    private void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController playerCharacter = collision.GetComponent<CharacterController>();
        if (playerCharacter != null)
        {
            if (_levelManager != null)
            {
                _levelManager.TransitionToEndpoint(_destinationRoomId, _destinationEndpointId);
            }
        }

    }

}
