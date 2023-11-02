using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomTeleporter : MonoBehaviour
{
    public string _destinationRoomId = "";
    public string _destinationEndpointId = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryTeleportPlayer(collision);
    }

    private void TryTeleportPlayer(Collider2D collision)
    {
        CharacterController playerCharacter = collision.GetComponent<CharacterController>();
        if (playerCharacter != null)
        {
            if (LevelManager.Instance != null)
                LevelManager.Instance.TransitionToEndpoint(_destinationRoomId, _destinationEndpointId);
            else
                Debug.LogWarning($"{gameObject.name} tried to teleport player, but has no level manager instance.");
        }
    }
}
