using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnManager : MonoBehaviour
{

    //public List<RoomSpawner> spawnerQueue = new List<RoomSpawner>();
    public Queue<RoomSpawner> spawnerQueue = new Queue<RoomSpawner>();
    public float spawnDelay = 0.5f;

    private float timer = 0;
    public bool readyToSpawn;


    void Start()
    {
        //InvokeRepeating("SpawnRoomInQueue", 0.5f, 0.1f);
        readyToSpawn = true;
        
    }

    void Update()
    {
        if(spawnerQueue.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay && readyToSpawn)
            {
                readyToSpawn = false;
                SpawnRoomInQueue();
                timer = 0;
            }            
        }
    }

    void SpawnRoomInQueue()
    {
        //spawnerQueue[0].Spawn();
        //spawnerQueue.RemoveAt(0);
        spawnerQueue.Dequeue().Spawn();
        
    }
}
