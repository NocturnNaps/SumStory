using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int NumberOfRooms;
    [SerializeField] GameObject[] FembootersRooms;
    [SerializeField] GameObject[] Enemies;
    GameObject currentRoom;
    GameObject[] currentRooms;
    GameObject player;
    GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        currentRoom = FindObjectOfType<Room>().gameObject;
        NumberOfRooms++;
        player = FindObjectOfType<Player>().gameObject;
    }

    public GameObject SpawnRoom(Vector3 position)
    {
        NumberOfRooms++;
        var NewRoom = Instantiate(FembootersRooms[Random.Range(0, FembootersRooms.Length)], position, Quaternion.identity) as GameObject;
        NewRoom.transform.parent = transform;
        return NewRoom;
    }
}
