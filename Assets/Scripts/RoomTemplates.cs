using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] startingRooms;
    public GameObject closedroom;
    private Vector3 spacing = new Vector3(0, 0, 0);

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if(waitTime<=0 && spawnedBoss ==false)
        {
            //int i = 1;
            //while(rooms[rooms.Count - i] == null) { i++; }

            // new concept: when overlord spawnCount = 0, roomcount / 2 + (rand( 0 , roomcount/2)
            //this will get the middle spawned room and add some random number to it.

            //Instantiate(boss, rooms[rooms.Count - i].transform.position , Quaternion.identity);
            spawnedBoss= true;
            

        }
        else if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
    }
}
