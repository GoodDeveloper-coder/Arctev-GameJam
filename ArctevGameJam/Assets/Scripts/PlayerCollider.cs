using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        player.CollisionEnter(other);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        player.CollisionExit(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.TriggerEnter(other);
    }
}
