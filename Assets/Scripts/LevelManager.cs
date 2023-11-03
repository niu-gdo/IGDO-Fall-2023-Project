using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField, Tooltip("ID of the default room to spawn in.")]
    private string _spawnRoomId = "";
    [SerializeField, Tooltip("ID of the default endpoint to spawn in.")]
    private string _spawnEndpointId = "";

    public static LevelManager Instance;    // Singleton instance

    private Dictionary<string, Room> RoomDictionary = new Dictionary<string, Room>();  

    /// <summary>
    /// Find a room in the level using a given roomId. Can return null.
    /// 
    /// </summary>
    /// <param name="roomId">ID of the room to find.</param>
    /// <returns>The room in the current level with the provided ID, NULL otherwise.</returns>
    public Room FindRoom(string roomId)
    {
        if (RoomDictionary.TryGetValue(roomId, out Room foundRoom))
        {
            return foundRoom;
        }
        else
        {
            Debug.LogWarning($"Could not find room ${roomId}.");
            return null;
        }

    }

    /// <summary>
    /// Find a Transition Endpoint in the given room by an Id.
    /// </summary>
    /// <param name="room">The room to search for endpoints in.</param>
    /// <param name="endpointId">The Id of the target room</param>
    /// <returns>Endpoint with matching Id, NULL otherwise.</returns>
    public TransitionEndpoint FindEndpoint(Room room, string endpointId)
    {
        if (room.roomEndpoints.TryGetValue(endpointId, out TransitionEndpoint foundEndpoint))
        {
            return foundEndpoint;
        }
        else
        {
            Debug.LogWarning($"Could not find transition at {room.Id}->{endpointId}.");
            return null;
        }
    }

    /// <summary>
    /// Find a transition endpoint given roomId and endpointId.
    /// </summary>
    /// <param name="roomId">Room Id containing the target endpoint</param>
    /// <param name="endpointId">Id of the target endpoint</param>
    /// <returns>Endpoint with given Id in the given room.</returns>
    public TransitionEndpoint FindEndpoint(string roomId, string endpointId)
    {
        Room targetRoom = FindRoom(roomId);
        if (targetRoom != null)
        {
            TransitionEndpoint targetEndpoint = FindEndpoint(targetRoom, endpointId);
            if (targetEndpoint != null)
            {
                return targetEndpoint;
            }
        }

        return null;
    }

    /// <summary>
    /// Find the room and an endpoint within it by the given Ids.
    /// </summary>
    /// <param name="roomId">Id of target room</param>
    /// <param name="endpointId">Id of target endpoint</param>
    /// <returns>Tuple containing the found room and endpoint. Both can be NULL.</returns>
    public (Room room, TransitionEndpoint endpoint) FindRoomAndEndpoint(string roomId, string endpointId)
    {
        (Room room, TransitionEndpoint endpoint) path = (null, null);

        path.room = FindRoom(roomId);
        if (path.room != null)
        {
            path.endpoint = FindEndpoint(path.room, endpointId);
        }

        return path;
    }

    public bool TransitionToEndpoint(string roomId, string endpointId)
    {
        // TODO: Consider caching player controller?
        CharacterController playerController = FindAnyObjectByType<CharacterController>();
        if (playerController == null)
        {
            Debug.LogWarning("LevelManager::TransitionToEndpoint - Could not find player.");
            return false;
        }

        (Room room, TransitionEndpoint endpoint) endpointPath = FindRoomAndEndpoint(roomId, endpointId);
        
        if (endpointPath.room != null && endpointPath.endpoint != null)
        {
            LoadSingleRoom(endpointPath.room);
            MovePlayerToEndpoint(playerController, endpointPath.endpoint);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Teleports the player to the given endpoint.
    /// </summary>
    public void MovePlayerToEndpoint(CharacterController playerCharacter, TransitionEndpoint endpoint)
    {
        // Make sure endpoint is not null before teleporting to it.
        if (endpoint != null)
            playerCharacter.transform.position = endpoint.transform.position;
        else
            Debug.LogWarning("LevelManager::MovePlayerToEndpoint - Tried to move player to null endpoint");
    }

    private void Awake()
    {
        InitializeSingleton();
        InitializeRoomDictionary();
    }

    // Awake occurs during initialization, but AFTER awake.
    private void Start()
    {
        LoadDefaultRoom();  // Needs to be done after rooms have initialized their endpoint dictionaries.
    }

    /// <summary>
    /// Populates the RoomDictionary with all GameObjects that have Room objects on them.
    /// </summary>
    private void InitializeRoomDictionary()
    {
        Room[] roomObjs = (Room[])FindObjectsByType(typeof(Room), FindObjectsSortMode.InstanceID);

        foreach (Room room in roomObjs)
        {
            RoomDictionary.Add(room.Id, room);
        }

        // TODO: Push a warning if no rooms are loaded.
        // TODO: Add validation to detect rooms with no endpoints or subrooms in their children (This should be illegal).
    }

    /// <summary>
    /// Construct this script's singleton.
    /// </summary>
    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Debug.Log("Init singleton");
            Instance = this;
        }
    }

    /// <summary>
    /// Calls Load() on the given room, and unload on all others, leaving only a single room loaded.
    /// </summary>
    /// <param name="targetRoom">The room to be loaded.</param>
    private void LoadSingleRoom(Room targetRoom)
    {
        foreach (var idRoomPair in RoomDictionary)
        {
            Room currentRoom = idRoomPair.Value;

            if (currentRoom == targetRoom)
                currentRoom.LoadRoom();
            else
                currentRoom.UnloadRoom();
        }
    }

    /// <summary>
    /// Loads the room given by the "SpawnPoint" Ids on this LevelManager instance.
    /// </summary>
    private void LoadDefaultRoom()
    {
        // TODO: Use the first room in the dict if spawn room is invalid.
        TransitionToEndpoint(_spawnRoomId, _spawnEndpointId);
    }
}
