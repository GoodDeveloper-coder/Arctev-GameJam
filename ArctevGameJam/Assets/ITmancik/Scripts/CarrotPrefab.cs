using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPrefab : MonoBehaviour
{
    private Rigidbody2D rb;
    public float carrotSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            GlobalScript.carrotsScore++;
            Destroy(this.gameObject);
        }
    }
}
