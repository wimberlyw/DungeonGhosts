using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 > Need bottom door
    // 2 > Need top door
    // 3 > Need left door
    // 4 > need right door


    private RoomTemplates templates;
    public int rand;
    private bool spawned = false;
    public Vector3 offset = new Vector3(-3, 0, 0);

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", .2f);

    }
    void Spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == 1)
            {

                //need to spawn room with BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position + offset, templates.bottomRooms[rand].transform.rotation);

            }
            else if (openingDirection == 2)
            {
                //spawn room with a TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position + offset, templates.topRooms[rand].transform.rotation);

            }
            else if (openingDirection == 3)
            {
                //spawn room with a LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position + offset, templates.leftRooms[rand].transform.rotation);


            }
            else if (openingDirection == 4)
            {
                //spawn room with a RIGHT door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position + offset, templates.rightRooms[rand].transform.rotation);


            }
           

            
        
    }
        spawned = true;


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned ==false && spawned == false)
            {
                //spawn blocked opening
                Instantiate(templates.closedroom, transform.position+offset, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

}