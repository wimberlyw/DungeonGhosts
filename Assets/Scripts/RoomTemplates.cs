using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedroom;
    private Vector3 spacing = new Vector3(3, 0, 0);

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if(waitTime<=0 && spawnedBoss ==false)
        {
            Instantiate(boss, rooms[rooms.Count - 1].transform.position + spacing, Quaternion.identity);
            spawnedBoss= true;

        }

        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
