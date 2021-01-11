using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlordScript : MonoBehaviour
{

    public Queue<SpawnPointScript> spawnerQueue = new Queue<SpawnPointScript>();
    public float spawnDelay = 0.02f;

    private float timer = 0;
    public bool readyToSpawn;
    public float spawnCount;

    [HideInInspector]
    public bool useDebugLines = false;
    public bool manualMapmaking = false;

    void Start()
    {
        readyToSpawn = true;

        //turn off auto scripts if in manual mode
        this.enabled = gameObject.GetComponent<RoomTemplates>().enabled = !manualMapmaking;    

    }

    void Update()
    {
        if(spawnerQueue.Count > 0 && readyToSpawn)
        {
            timer += Time.deltaTime;

            if (timer > spawnDelay)
            {
                readyToSpawn = false;
                SpawnRoomInQueue();
                timer = 0;
            }            
        }
    }

    void SpawnRoomInQueue()
    {
        SpawnPointScript nextSpawn = spawnerQueue.Dequeue();
        if(nextSpawn != null)
        {
            nextSpawn.Spawn();
        }
        else
        {
            readyToSpawn = true;
        }
    }

    public void AddRoomToQueue(SpawnPointScript roomToAdd)
    {
        spawnerQueue.Enqueue(roomToAdd);
    }
}
