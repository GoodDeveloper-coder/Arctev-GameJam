using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerYin;
    [SerializeField] private GameObject playerYang;
    [SerializeField] private GameObject backgroundYin;
    [SerializeField] private GameObject backgroundYang;
    [SerializeField] private GameObject iconYin;
    [SerializeField] private GameObject iconYang;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;

    private Rigidbody2D rigidBody;
    private bool onGround;
    private bool horizonFlipped;

    // Start is called before the first frame update
    void Start()
    {
        playerYang.SetActive(false);
        backgroundYin.SetActive(false);
        rigidBody = GetComponent<Rigidbody2D>();
        //onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                horizonFlipped = !horizonFlipped;
                playerYin.SetActive(!horizonFlipped);
                playerYang.SetActive(horizonFlipped);
                backgroundYin.SetActive(horizonFlipped);
                backgroundYang.SetActive(!horizonFlipped);
                iconYin.SetActive(horizonFlipped);
                iconYang.SetActive(!horizonFlipped);
                rigidBody.gravityScale *= -1;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) rigidBody.AddForce(Vector2.up * jumpForce * (horizonFlipped ? -1 : 1), ForceMode2D.Impulse);
        }
        //if (Mathf.Abs(walkInput) > 0.1f) rigidBody.AddForce(Vector2.right * walkInput * walkSpeed, ForceMode2D.Impulse);
        if (horizonFlipped ? rigidBody.velocity.y > 0 : rigidBody.velocity.y < 0) rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
        else if (horizonFlipped ? rigidBody.velocity.y < 0 : rigidBody.velocity.y > 0) rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!onGround && collider.gameObject != playerYin && collider.gameObject != playerYang) onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (onGround && collider.gameObject != playerYin && collider.gameObject != playerYang) onGround = false;
    }
}
