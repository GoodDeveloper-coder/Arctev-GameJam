using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    public Transform[] spawnPosWhite;
    public Transform[] spawnPosDark;

    public GameObject DarkPowerUpPrefab;
    public GameObject WhitePowerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.RandomRange(1, 2) == 1)
        {
            GameObject WhitePowerUp = Instantiate(DarkPowerUpPrefab, spawnPosWhite[Random.RandomRange(0, spawnPosWhite.Length)].position, Quaternion.identity);
            WhitePowerUp.transform.parent = this.transform;
        }

        if (Random.RandomRange(1, 2) == 1)
        {
            GameObject DarkPowerUp = Instantiate(WhitePowerUpPrefab, spawnPosDark[Random.RandomRange(0, spawnPosDark.Length)].position, Quaternion.identity);
            DarkPowerUp.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
