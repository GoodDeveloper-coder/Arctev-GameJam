using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPrefab : MonoBehaviour
{
    private TimeManager timeManager;
    private AudioSource CollectSound;
    public float CarrotAddScore = 20f;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = GameObject.Find("Time Manager").GetComponent<TimeManager>();
        CollectSound = GameObject.Find("CollectCarrotSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timeManager.AddScore(CarrotAddScore);
            GlobalScript.carrotsScore++;
            CollectSound.Play();
            Destroy(this.gameObject);
        }
    }
}
