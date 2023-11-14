using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public GameObject cam;

    public float parallaxSpeed;
    float startPosX;

    public GeneralParralax GeneralParralaxScript;

    private Transform SpawnParralaxBG;

    // Start is called before the first frame update
    void Start()
    {
        SpawnParralaxBG = GameObject.FindGameObjectWithTag("ParralaxSpawnPos").GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("FakeCam");
        startPosX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distX = (cam.transform.position.x * (1 - parallaxSpeed));
        transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("StartCoroutine(Destroy())");
        StartCoroutine(Destroy());
        if (other.tag == "ParralaxTrigger")
        {
            Debug.Log("StartCoroutine(Destroy())");
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        GeneralParralaxScript.Destroy();
    }
}
