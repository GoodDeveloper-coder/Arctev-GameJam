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
        player.CollisionEnter(gameObject);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        player.CollisionExit(gameObject);
    }
}
