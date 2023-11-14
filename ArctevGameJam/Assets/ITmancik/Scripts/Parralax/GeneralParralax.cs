using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralParralax : MonoBehaviour
{
    private Transform SpawnParralaxBG;

    public GameObject ParralaxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnParralaxBG = GameObject.FindGameObjectWithTag("ParralaxSpawnPos").GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveBG()
    {
        this.transform.position = SpawnParralaxBG.transform.position;
    }

    public void Destroy()
    {
        Instantiate(ParralaxPrefab, SpawnParralaxBG.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
