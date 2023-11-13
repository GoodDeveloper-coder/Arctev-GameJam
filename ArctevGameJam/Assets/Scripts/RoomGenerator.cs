using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject[] roomPrefabs;

    [SerializeField] GameObject startRoom;

    [SerializeField] float roomLength;

    private GameObject currentRoom;
    private GameObject nextRoom;

    private int currentRoomIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentRoomIndex = Random.Range(0, roomPrefabs.Length);
        nextRoom = Instantiate(roomPrefabs[currentRoomIndex], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject != player) return;
        transform.position += Vector3.right * roomLength;
        Destroy(currentRoom);
        currentRoom = nextRoom;
        int r;
        do r = Random.Range(0, roomPrefabs.Length);
        while (currentRoomIndex != r);
        currentRoomIndex = r;
        nextRoom = Instantiate(roomPrefabs[currentRoomIndex], transform.position, transform.rotation);
    }
}