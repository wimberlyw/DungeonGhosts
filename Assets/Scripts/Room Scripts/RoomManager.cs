﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    /*------------ PURPOSE ------------
     * This script spawns rooms sequentially to keep the spawners from overloading.
     * It keeps a queue of all the rooms to spawn by accepting each spawnpoint from recent spawned rooms.
      ---------------------------------*/

    public GameObject[] roomSpawners;
    //bool hasCollided = false;
    bool doOnce = true;
    bool manualMapmaking;

    // Start is called before the first frame update
    void Start()
    {
        manualMapmaking = GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<OverlordScript>().manualMapmaking;

        if (manualMapmaking == true)
        {
            foreach (GameObject spawner in roomSpawners)
            {
                Destroy(spawner);
            }
        }

        this.enabled = !manualMapmaking;
    }

    // Update is called once per frame
    void Update()
    {

        if (doOnce == true)
        {
            doOnce = false;

            // --- Consalidated 'add room' to the room overlord
            GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<RoomTemplates>().rooms.Add(gameObject);
                

            //--- for each spawn we have in this room add it to the overlord to be spawned if possible
            foreach (GameObject spawner in roomSpawners)
            {
                if(spawner != null)
                    GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<OverlordScript>().AddRoomToQueue(spawner.GetComponent<SpawnPointScript>());

            }

        }
    }

    //Runs before update so we can check if something is colliding first
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.transform.root != gameObject.transform.root) && (other.tag != "SpawnPoint") && (other.tag != "Player"))
        {
            hasCollided = true;
            Debug.Log(gameObject.name + " has collided with " + other.gameObject.name);
            CollisionCheck(); 
            // WORKING ON CHECKING COLLISION DURING ROOM CHECK
        }
    }

    public void CollisionCheck()
    {
        Debug.Log(doOnce);
        if (doOnce == true)
        {
            Destroy(gameObject);
        }
    }
}

