using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetector : MonoBehaviour
{
    Door myDoor;
    GameObject myDoorObject;
    bool FoundDoor = true;
    Door OtherDoor;
    // Start is called before the first frame update
    void Start()
    {
        myDoor = GetComponentInParent<Door>();
        myDoorObject = myDoor.gameObject;
        if (!myDoor || !myDoorObject)
        {
            Debug.Log("lol door broken");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OtherDoor = other.GetComponent<Door>();
        if (!OtherDoor) {return;}
        Debug.Log("a room entered");
        while(FoundDoor)
        {
            Debug.Log("Checking for Room");
            myDoor.SetRoom(OtherDoor.ThisRoom, OtherDoor.ThisRoomObject);
        }
        Debug.Log("Setted the Room");
    }
}
