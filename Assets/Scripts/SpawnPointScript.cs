﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
   [Tooltip(
        "1 - Attaches to BOTTOM door\n" +
        "2 - Attaches to TOP door\n" +
        "3 - Attaches to LEFT door\n" +
        "4 - Attaches to RIGHT door")]
    public int openingDirection;

    public int rand;

    private bool debugLinesEnabled;
    private bool spawned = false;
    private RoomTemplates templates;
    private GameObject spawnedRoom;
    private OverlordScript roomManager;
    private Transform spawnedDoor;
    

    void Start()
    {

        GameObject roomOverlord = GameObject.FindGameObjectWithTag("RoomOverlord");
        templates = roomOverlord.GetComponent<RoomTemplates>(); 
        roomManager = roomOverlord.GetComponent<OverlordScript>();

        debugLinesEnabled = roomManager.useDebugLines;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root != transform.root)
        {
            spawned = true;
            //roomManager.readyToSpawn = true;
            //Debug.LogError("Deleted " + gameObject.name);
            Destroy(gameObject);
        }
    }

    public void Spawn()
    {
        if (gameObject == null)
        {
            roomManager.readyToSpawn = true;
            return;
        }
        

        if(spawned == false)
        {

            if (openingDirection == 1)
            {
                if (templates.bottomRooms[rand] != null)
                {
                    //need to spawn room with BOTTOM door
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    spawnedRoom = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    spawnedDoor = spawnedRoom.transform.Find("B_Door");
                    spawnedDoor.GetComponent<SpawnPointScript>().spawned = true;
                    spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;
                }
                else
                    Debug.LogError("There are no rooms in the BOTTOM list!");
            }
            else if (openingDirection == 2)
            {
                if (templates.topRooms[rand] != null)
                {
                    //spawn room with a TOP door
                    rand = Random.Range(0, templates.topRooms.Length);
                    spawnedRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    spawnedDoor = spawnedRoom.transform.Find("T_Door");

                    spawnedDoor.GetComponent<SpawnPointScript>().spawned = true;
                    spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;
                }
                else
                    Debug.LogError("There are no rooms in the TOP list!");
            }
            else if (openingDirection == 3)
            {
                if (templates.leftRooms[rand] != null)
                {
                    //spawn room with a LEFT door
                    rand = Random.Range(0, templates.leftRooms.Length);
                    spawnedRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    spawnedDoor = spawnedRoom.transform.Find("L_Door");
                    spawnedDoor.GetComponent<SpawnPointScript>().spawned = true;
                    spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;
                }
                else
                    Debug.LogError("There are no rooms in the LEFT list!");

            }
            else if (openingDirection == 4)
            {
                if (templates.rightRooms[rand] != null)
                {
                    //spawn room with a RIGHT door
                    rand = Random.Range(0, templates.rightRooms.Length);
                    spawnedRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    spawnedDoor = spawnedRoom.transform.Find("R_Door");
                    spawnedDoor.GetComponent<SpawnPointScript>().spawned = true;
                    spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;
                }
                else
                    Debug.LogError("There are no rooms in the RIGHT list!");
            }

            // THIS IS FOR DEBUG ONLY -- REMOVE ME!
            if (debugLinesEnabled)
            {
                if (gameObject.transform.root.GetComponent<LineRenderer>() != null)
                {
                    LineRenderer lr = gameObject.transform.root.gameObject.AddComponent<LineRenderer>();

                    lr.widthMultiplier = 0.5f;
                    lr.SetPosition(0, gameObject.transform.root.position);
                    lr.SetPosition(1, spawnedRoom.transform.position);
                }
            }
        }
        roomManager.readyToSpawn = true;
        Destroy(gameObject);

    }
}