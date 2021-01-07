using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] roomSpawners;
    bool isRoomOverlapping = false;
    bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doOnce == true)
        {
            doOnce = false;

            if (!isRoomOverlapping)
            {
                // --- Consalidated 'add room' to the room overlord
                GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<RoomTemplates>().rooms.Add(gameObject);
                

                //--- for each spawn we have in this room add it to the overlord to be spawned if possible
                foreach (GameObject room in roomSpawners)
                {
                    GameObject.FindGameObjectWithTag("RoomOverlord").GetComponent<OverlordScript>().AddRoomToQueue(room.GetComponent<SpawnPointScript>());

                }
            }
            else
            {
                
                Destroy(gameObject); //destroy the object if its colliding with something
            }
        }
    }

    //Runs before update so we can check if something is colliding first
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root != gameObject.transform.root)
        {
            Debug.Log(gameObject.name + " has collided with " + other.gameObject.name);
            isRoomOverlapping = true;
        }
    }
}
