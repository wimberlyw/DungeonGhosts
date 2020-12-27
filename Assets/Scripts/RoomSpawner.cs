using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
   [Tooltip(
        "1 - Attaches to BOTTOM door\n" +
        "2 - Attaches to TOP door\n" +
        "3 - Attaches to LEFT door\n" +
        "4 - Attaches to RIGHT door")]
    public int openingDirection;

    public int rand;
    public Vector3 offset = new Vector3(0, 0, 0);
    public bool debugLinesEnabled = false;

    private bool spawned = false;
    private RoomTemplates templates;
    private GameObject spawnedRoom;
    private RoomSpawnManager roomManager;
    private Transform spawnedDoor;
    

    void Start()
    {
        
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //Invoke("Spawn", 0.1f);

        
        roomManager = GameObject.Find("RoomOverlord").GetComponent<RoomSpawnManager>();
        roomManager.spawnerQueue.Enqueue(this);
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.root != transform.root)
        {
            Debug.Log(">>>>> I HIT " + other.transform.root.name);
            if (other.CompareTag("Wall") || other.CompareTag("SpawnPoint"))
            {
                Destroy(gameObject);
            }

            spawned = true;
            roomManager.readyToSpawn = true;


            //if(other.GetComponent<RoomSpawner>().spawned ==false && spawned == false)
            // {
            //spawn blocked opening
            //Instantiate(templates.closedroom, transform.position+offset, Quaternion.identity);
            //Destroy(gameObject);
            //}
        }

    }

    public void Spawn()
    {
        
        if(spawned == false)
        {
            
            if (openingDirection == 1)
            {

                //need to spawn room with BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length);
                spawnedRoom = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                spawnedDoor = spawnedRoom.transform.Find("B_Door");
                spawnedDoor.GetComponent<RoomSpawner>().spawned = true;
                spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;

            }
            else if (openingDirection == 2)
            {
                //spawn room with a TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                spawnedRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                spawnedDoor = spawnedRoom.transform.Find("T_Door");

                spawnedDoor.GetComponent<RoomSpawner>().spawned = true;
                spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;
            }
            else if (openingDirection == 3)
            {
                //spawn room with a LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                spawnedRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                spawnedDoor = spawnedRoom.transform.Find("L_Door");
                spawnedDoor.GetComponent<RoomSpawner>().spawned = true;
                spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;

            }
            else if (openingDirection == 4)
            {
                //spawn room with a RIGHT door
                rand = Random.Range(0, templates.rightRooms.Length);
                spawnedRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                spawnedDoor = spawnedRoom.transform.Find("R_Door");
                spawnedDoor.GetComponent<RoomSpawner>().spawned = true;
                spawnedRoom.transform.position = spawnedRoom.transform.position - spawnedDoor.localPosition;

            }

            // THIS IS FOR DEBUG ONLY -- REMOVE ME!
            if (debugLinesEnabled)
            {
                if (gameObject.transform.root.GetComponent<LineRenderer>() == null)
                {
                    LineRenderer lr = gameObject.transform.root.gameObject.AddComponent<LineRenderer>();

                    lr.widthMultiplier = 0.5f;
                    lr.SetPosition(0, gameObject.transform.root.position);
                    lr.SetPosition(1, spawnedRoom.transform.position);
                }
            }


        }
        spawned = true;
        roomManager.readyToSpawn = true;

        


    }
    

}