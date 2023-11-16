using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPrefab : MonoBehaviour
{
    private Rigidbody2D rb;
    public float carrotSpeed = 0.5f;

    private TimeManager timeManager;

    public float CarrotAddScore = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeManager = GameObject.Find("Time Manager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * -carrotSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timeManager.AddScore(CarrotAddScore);
            GlobalScript.carrotsScore++;
            Destroy(this.gameObject);
        }
    }
}
