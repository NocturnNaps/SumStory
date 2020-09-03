using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Values
    [SerializeField] float DistanceBetweenRooms = 10;
    [SerializeField] public GameObject SpawnPoint;

    // Cache
    CapsuleCollider2D myCapsuleCollider2D;
    GameObject otherRoomSpawnPoint;
    RoomManager roomManager;
    public Room ThisRoom;
    public GameObject ThisRoomObject;
    public Room room;
    public GameObject roomObject;
    public Vector3 CameraPosition;
    GameObject otherDoor;

    // States
    [SerializeField] bool RightEnter;
    [SerializeField] bool LeftEnter;
    [SerializeField] bool UpperEnter;
    [SerializeField] bool DownEnter;
    bool CreatedRoom = false;

    public void SetSpawnPoint(GameObject newSpawnPoint)
    {
        otherRoomSpawnPoint = newSpawnPoint;
    }

    public void SetRoom(Room TheRoom, GameObject theObjectRoom)
    {
        Debug.Log("Set the roomm");
        CreatedRoom = true;
        room = TheRoom;
        roomObject = theObjectRoom;

        if (RightEnter)
        {
            otherDoor = room.LeftDoor;
        }
        else if (LeftEnter)
        {
            otherDoor = room.RightDoor;
        }
        else if (UpperEnter)
        {
            otherDoor = room.DownDoor;
        }
        else if (DownEnter)
        {
            otherDoor = room.UpperDoor;
        }
    }

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        myCapsuleCollider2D = GetComponentInChildren<CapsuleCollider2D>();
        ThisRoom = GetComponentInParent<Room>();
        ThisRoomObject = ThisRoom.GetRoomObject();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Player>()) { return; }
        if (!otherDoor)
        {
            Debug.Log("Created a Room");
            CreateRoom();
            EnterRoom();
        }
        else
        {
            Debug.Log("Entered a Room");
            EnterRoom();
        }

        void EnterRoom()
        {
            if (!otherDoor) {return;}
            other.transform.position = otherDoor.transform.position;
            Camera.main.transform.position = CameraPosition;
        }

        void CreateRoom()
        {
            CreatedRoom = true;
            if (RightEnter)
            {
                roomObject = roomManager.SpawnRoom(
                    new Vector3(ThisRoomObject.transform.position.x + DistanceBetweenRooms, ThisRoomObject.transform.position.y, 0));
                room = roomObject.GetComponent<Room>();
                CameraPosition = roomObject.transform.position + new Vector3(10.98495f,-2.89f,-149.2843f);
                otherDoor = room.LeftDoor;
            }
            else if (LeftEnter)
            {
                roomObject = roomManager.SpawnRoom(
                    new Vector3(ThisRoomObject.transform.position.x - DistanceBetweenRooms, ThisRoomObject.transform.position.y, 0));
                room = roomObject.GetComponent<Room>();
                CameraPosition = roomObject.transform.position + new Vector3(10.98495f,-2.89f,-149.2843f);
                otherDoor = room.RightDoor;
            }
            else if (UpperEnter)
            {
                roomObject = roomManager.SpawnRoom(
                    new Vector3(ThisRoomObject.transform.position.x, ThisRoomObject.transform.position.y + DistanceBetweenRooms, 0));
                room = roomObject.GetComponent<Room>();
                CameraPosition = roomObject.transform.position + new Vector3(10.98495f,-2.89f,-149.2843f);
                otherDoor = room.DownDoor;
            }
            else if (DownEnter)
            {
                roomObject = roomManager.SpawnRoom(
                    new Vector3(ThisRoomObject.transform.position.x, ThisRoomObject.transform.position.y - DistanceBetweenRooms, 0));
                room = roomObject.GetComponent<Room>();
                CameraPosition = roomObject.transform.position + new Vector3(10.98495f,-2.89f,-149.2843f);
                otherDoor = room.UpperDoor;
            }
        }
    }
}
