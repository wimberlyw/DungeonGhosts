using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    /*------------ PURPOSE ------------
     * This script spawns rooms sequentially to keep the spawners from overloading.
     * It keeps a queue of all the rooms to spawn by accepting each spawnpoint from recent spawned rooms.
      ---------------------------------*/

    public GameObject[] roomSpawners;
    bool doOnce = true;
    bool manualMapmaking;

    public float instantiateTime;

    void Start()
    {
        manualMapmaking = GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<OverlordScript>().manualMapmaking;
        instantiateTime = Time.timeSinceLevelLoad;

        if (manualMapmaking == true)
        {
            foreach (GameObject spawner in roomSpawners)
                Destroy(spawner);
        }

        this.enabled = !manualMapmaking;
    }

    void Update()
    {
        if (gameObject == null) return;
        if (doOnce == true)
        {          
            // --- Consalidated 'add room' to the room overlord
            GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<RoomTemplates>().rooms.Add(gameObject);
                

            //--- for each spawn we have in this room add it to the overlord to be spawned if possible
            foreach (GameObject spawner in roomSpawners)
            {
                if(spawner != null)
                    GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<OverlordScript>().AddRoomToQueue(spawner.GetComponent<SpawnPointScript>());
            }

            doOnce = false; //only let this function run once
        }
    }

    //Runs before update so we can check if something is colliding and delete the youngest of the 2 rooms
    private void OnTriggerEnter2D(Collider2D other)
    {

        if ((other.transform.root != gameObject.transform.root) && (other.transform.root.tag == "Rooms"))
        {
            float otherAge = other.transform.root.GetComponent<RoomManager>().GetRoomAge();

            //Debug.Log(gameObject.name + " (age: " + (int)GetRoomAge() + ") has collided with " + other.transform.root.name + " (age: " + (int)otherAge + ")");       
            if(GetRoomAge() < otherAge)
            {
                Destroy(gameObject);
            }
            // WORKING ON CHECKING COLLISION DURING ROOM CHECK
        }
    }

    //Gets the age of the current room since the scene was loaded
    public float GetRoomAge()
    {
        return Time.timeSinceLevelLoad - instantiateTime;
    }

}

