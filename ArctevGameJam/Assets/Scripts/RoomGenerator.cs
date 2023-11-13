using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject[] roomPrefabs;

    [SerializeField] GameObject startRoom;

    [SerializeField] float roomLength;

    private GameObject previousRoom;
    private GameObject currentRoom;
    private GameObject nextRoom;

    private int currentRoomIndex;
    private float speed;
    private bool yin;

    // Start is called before the first frame update
    void Start()
    {
        currentRoomIndex = Random.Range(0, roomPrefabs.Length);
        currentRoom = startRoom;
        nextRoom = Instantiate(roomPrefabs[currentRoomIndex], transform.position + Vector3.right * roomLength, transform.rotation);
        yin = true;
        //SetHorizon();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;
        if (previousRoom != null) previousRoom.transform.position -= Vector3.right * speed * Time.deltaTime;
        currentRoom.transform.position -= Vector3.right * speed * Time.deltaTime;
        nextRoom.transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject != player) return;
        transform.position += Vector3.right * roomLength;
        if (previousRoom != null) Destroy(previousRoom);
        previousRoom = currentRoom;
        currentRoom = nextRoom;
        int r;
        do r = Random.Range(0, roomPrefabs.Length);
        while (currentRoomIndex != r);
        currentRoomIndex = r;
        nextRoom = Instantiate(roomPrefabs[currentRoomIndex], transform.position + Vector3.right * roomLength, transform.rotation);
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void Flip()
    {
        yin = !yin;
        //SetHorizon();
    }

    private void SetHorizon()
    {
        if (previousRoom != null)
        {
            previousRoom.transform.Find("Yin").gameObject.SetActive(yin);
            previousRoom.transform.Find("Yang").gameObject.SetActive(!yin);
        }
        currentRoom.transform.Find("Yin").gameObject.SetActive(yin);
        currentRoom.transform.Find("Yang").gameObject.SetActive(!yin);
        nextRoom.transform.Find("Yin").gameObject.SetActive(yin);
        nextRoom.transform.Find("Yang").gameObject.SetActive(!yin);
    }
}
