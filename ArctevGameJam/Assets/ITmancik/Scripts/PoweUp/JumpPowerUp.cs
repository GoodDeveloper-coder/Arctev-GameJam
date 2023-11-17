using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    private Player player;
    private AudioSource CollectSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        CollectSound = GameObject.Find("CollectPowerUpSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            player.jumpForce += player.jumpForce / 10;
            CollectSound.Play();
            Destroy(this.gameObject);
        }
    }
}
