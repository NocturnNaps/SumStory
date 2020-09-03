using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    public GameObject RightDoor;
    public GameObject LeftDoor;
    public GameObject UpperDoor;
    public GameObject DownDoor;

    [Header("Actual Doors")]
    public Door ActualRightDoor;
    public Door ActualLeftDoor;
    public Door ActualUpperDoor;
    public Door ActualDownDoor;

    //cache
    Room overRoom;

    void OnTriggerEnter(Collider other)
    {
        overRoom = other.GetComponent<Room>();
        if (other.GetComponent<Player>()) {return;}

        Destroy(gameObject);
    }

    public GameObject GetRoomObject()
    {
        return gameObject;
    }
}
