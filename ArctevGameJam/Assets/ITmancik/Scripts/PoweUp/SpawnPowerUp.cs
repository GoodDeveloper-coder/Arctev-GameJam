using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupYinInstances;
    [SerializeField] private GameObject[] powerupYangInstances;

    [SerializeField] private GameObject[] carrotYinInstances;
    [SerializeField] private GameObject[] carrotYangInstances;

    [SerializeField] private int powerupTypes;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++) Destroy((Random.Range(0, 2) == 0 ? carrotYinInstances : carrotYangInstances)[i]);
        GameObject powerup;
        int r = Random.Range(0, (powerupYinInstances.Length + powerupYangInstances.Length) * 2);
        for (int i = 0; i < powerupYinInstances.Length; i++)
        {
            if (r == i) powerupYinInstances[i].GetComponent<JumpPowerUp>().SetPowerupType(Random.Range(0, powerupTypes));
            else Destroy(powerupYinInstances[i]);
        }
        for (int i = 0; i < powerupYangInstances.Length; i++)
        {
            if (r == i + powerupYinInstances.Length) powerupYangInstances[i].GetComponent<JumpPowerUp>().SetPowerupType(Random.Range(0, powerupTypes));
            else Destroy(powerupYangInstances[i]);
        }

        /*
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
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
