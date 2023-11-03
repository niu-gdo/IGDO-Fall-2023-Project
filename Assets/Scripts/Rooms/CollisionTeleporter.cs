using UnityEngine;

/// <summary>
/// A teleporter that moves the player to a destination endpoint
/// when they enter an attached Trigger collider.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class CollisionTeleporter : MonoBehaviour, ITeleporter
{
    [SerializeField] private string _destinationRoomId = "";
    [SerializeField] private string _destinationEndpointId = "";

    public string DestinationRoomId { get => _destinationRoomId; }
    public string DestinationEndpointId { get => _destinationEndpointId; }

    public (string roomId, string endpointId) GetDestination(string roomId, string endpointId) => (DestinationRoomId, DestinationEndpointId);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController>() != null)
            TryTeleportPlayer();
    }

    /// <summary>
    /// Attempts to use the LevelManager to teleport to a room's endpoint.
    /// </summary>
    private void TryTeleportPlayer()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.TransitionToEndpoint(_destinationRoomId, _destinationEndpointId);
        else
            Debug.LogWarning($"{gameObject.name} tried to teleport player, but has no level manager instance.");
    }
}
