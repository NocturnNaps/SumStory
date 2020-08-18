using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float DistanceBetweenRooms = 10;
    RoomManager roomManager;
    Room room;
    GameObject roomObject;
    [SerializeField] bool RightEnter;
    [SerializeField] bool LeftEnter;
    [SerializeField] bool UpperEnter;
    [SerializeField] bool DownEnter;

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Player>()) {return;}
        if(roomObject)
        {
            if (RightEnter)
            {
                other.transform.position = room.LeftDoor.transform.position;
            }
            else if (LeftEnter)
            {
                other.transform.position = room.RightDoor.transform.position;
            }
            else if (UpperEnter)
            {
                other.transform.position = room.DownDoor.transform.position;
            }
            else if (DownEnter)
            {
                other.transform.position = room.UpperDoor.transform.position;
            }
        }
        else
        {
            if (RightEnter)
            {
                roomObject = roomManager.SpawnRoom(new Vector3(transform.position.x - DistanceBetweenRooms,transform.position.y,0));
                room = roomObject.GetComponent<Room>();
            }
            else if (LeftEnter)
            {
                roomObject = roomManager.SpawnRoom(new Vector3(transform.position.x + DistanceBetweenRooms,transform.position.y,0));
                room = roomObject.GetComponent<Room>();
            }
            else if (UpperEnter)
            {
                roomObject = roomManager.SpawnRoom(new Vector3(transform.position.x,transform.position.y - DistanceBetweenRooms,0));
                room = roomObject.GetComponent<Room>();
            }
            else if (DownEnter)
            {
                roomObject = roomManager.SpawnRoom(new Vector3(transform.position.x,transform.position.y + DistanceBetweenRooms,0));
                room = roomObject.GetComponent<Room>();
            }
        }
    }
}
