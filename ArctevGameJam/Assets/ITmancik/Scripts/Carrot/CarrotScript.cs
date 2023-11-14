using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : MonoBehaviour
{
    public Transform[] SpawnPositionCarrots;

    public GameObject Carrot;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCarrots());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCarrots()
    {
        Instantiate(Carrot, SpawnPositionCarrots[Random.Range(0, SpawnPositionCarrots.Length)].position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(SpawnCarrots());
    }
}
