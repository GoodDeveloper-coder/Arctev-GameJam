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
    [SerializeField] private float smallJumpForce;
    [SerializeField] private float bigJumpForce;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float maxInputTime;

    private Rigidbody2D rigidBody;
    private bool onGround;
    private bool horizonFlipped;
    private float jumpInputTime;
    private float flipInputTime;

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
        if (jumpInputTime > 0) jumpInputTime -= Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) jumpInputTime = maxInputTime;
        if (flipInputTime > 0) flipInputTime -= Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) flipInputTime = maxInputTime;
    }

    void FixedUpdate()
    {
        //if (Mathf.Abs(walkInput) > 0.1f) rigidBody.AddForce(Vector2.right * walkInput * walkSpeed, ForceMode2D.Impulse);
        if (horizonFlipped ? rigidBody.velocity.y > 0 : rigidBody.velocity.y < 0) rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
        else if (horizonFlipped ? rigidBody.velocity.y < 0 : rigidBody.velocity.y > 0) rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!onGround && collider.gameObject != playerYin && collider.gameObject != playerYang)
        {
            onGround = true;
            if (flipInputTime > 0)
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
            rigidBody.AddForce(Vector2.up * (jumpInputTime > 0 ? bigJumpForce : smallJumpForce) * (horizonFlipped ? -1 : 1), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (onGround && collider.gameObject != playerYin && collider.gameObject != playerYang) onGround = false;
    }
}
