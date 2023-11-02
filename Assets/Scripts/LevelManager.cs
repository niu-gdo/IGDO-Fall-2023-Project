using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Dictionary<string, Room> RoomDictionary = new Dictionary<string, Room>();  // <RoomID, Room>

    [SerializeField] private string _spawnRoomId = "";
    [SerializeField] private string _spawnEndpointId = "";

    public static LevelManager Instance;

    public Room GetRoomById(string roomId)
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
    public TransitionEndpoint GetEndpointByRoomAndId(Room room, string endpointId)
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

    public bool TransitionToEndpoint(string roomId, string endpointId)
    {
        CharacterController playerController = FindAnyObjectByType<CharacterController>();
        if (playerController == null) 
            return false;

        Room targetRoom = GetRoomById(roomId);
        if (targetRoom != null)
        {
            TransitionEndpoint targetEndpoint = GetEndpointByRoomAndId(targetRoom, endpointId);
            if (targetEndpoint)
            {
                LoadSingleRoom(targetRoom);
                MovePlayerToEndpoint(playerController, targetEndpoint);
                return true;
            }
        }

        return false;
    }

    public void MovePlayerToEndpoint(CharacterController playerCharacter, TransitionEndpoint endpoint)
    {
        // Make sure endpoint is not null before teleporting to it.
        if (endpoint != null)
            playerCharacter.transform.position = endpoint.transform.position;
    }

    private void Awake()
    {
        InitializeSingleton();
        InitializeRoomDictionary();
    }

    // Start occurs after awake
    private void Start()
    {
        LoadDefaultRoom();
    }

    private void InitializeRoomDictionary()
    {
        Room[] roomObjs = (Room[])FindObjectsByType(typeof(Room), FindObjectsSortMode.InstanceID);

        foreach (Room room in roomObjs)
        {
            RoomDictionary.Add(room.Id, room);
        }
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

    private void LoadDefaultRoom()
    {
        TransitionToEndpoint(_spawnRoomId, _spawnEndpointId);
    }
}
