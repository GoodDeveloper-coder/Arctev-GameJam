using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] roomPrefabs;

    [SerializeField] private GameObject startRoom;

    [SerializeField] private float roomLength;

    private GameObject previousRoom;
    private GameObject currentRoom;
    private GameObject nextRoom;

    private int currentRoomIndex;
    private float speed;
    private bool yin;

    // Start is called before the first frame update
    void Start()
    {
        currentRoomIndex = -1;
        nextRoom = startRoom;
        SetYin(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;
        if (previousRoom != null) previousRoom.transform.position -= Vector3.right * speed * Time.deltaTime;
        if (currentRoom != null) currentRoom.transform.position -= Vector3.right * speed * Time.deltaTime;
        nextRoom.transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCollider>() == null) return;
        transform.position += Vector3.right * roomLength;
        if (previousRoom != null) Destroy(previousRoom);
        previousRoom = currentRoom;
        currentRoom = nextRoom;
        int r;
        do r = Random.Range(0, roomPrefabs.Length);
        while (currentRoomIndex == r);
        currentRoomIndex = r;
        nextRoom = Instantiate(roomPrefabs[currentRoomIndex], transform.position, transform.rotation);
        SetYin(yin);
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void HideAll()
    {
        if (previousRoom != null)
        {
            previousRoom.transform.Find("Yin").gameObject.SetActive(false);
            previousRoom.transform.Find("Yang").gameObject.SetActive(false);
        }
        if (currentRoom != null)
        {
            currentRoom.transform.Find("Yin").gameObject.SetActive(false);
            currentRoom.transform.Find("Yang").gameObject.SetActive(false);
        }
        nextRoom.transform.Find("Yin").gameObject.SetActive(false);
        nextRoom.transform.Find("Yang").gameObject.SetActive(false);
    }

    public void SetYin(bool y)
    {
        yin = y;
        if (previousRoom != null)
        {
            previousRoom.transform.Find("Yin").gameObject.SetActive(yin);
            previousRoom.transform.Find("Yang").gameObject.SetActive(!yin);
        }
        if (currentRoom != null)
        {
            currentRoom.transform.Find("Yin").gameObject.SetActive(yin);
            currentRoom.transform.Find("Yang").gameObject.SetActive(!yin);
        }
        nextRoom.transform.Find("Yin").gameObject.SetActive(yin);
        nextRoom.transform.Find("Yang").gameObject.SetActive(!yin);
    }
}
