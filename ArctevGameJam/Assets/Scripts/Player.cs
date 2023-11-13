using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerYin;
    [SerializeField] private GameObject playerYang;
    [SerializeField] private GameObject colliderYin;
    [SerializeField] private GameObject colliderYang;
    [SerializeField] private GameObject backgroundYin;
    [SerializeField] private GameObject backgroundYang;
    [SerializeField] private GameObject iconYin;
    [SerializeField] private GameObject iconYang;
    [SerializeField] private TextMeshProUGUI[] scoreText;

    [SerializeField] private float initialWalkSpeed;
    [SerializeField] private float walkSpeedIncrementPerSecond;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;

    private Rigidbody2D rbYin;
    private Rigidbody2D rbYang;
    private PlayerAnimation animator;
    private bool onGround;
    private bool horizonFlipped;
    private float walkSpeed;
    private float score;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerYang.SetActive(false);
        backgroundYin.SetActive(false);
        rbYin = playerYin.GetComponent<Rigidbody2D>();
        rbYang = playerYang.GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimation>();
        foreach (TextMeshProUGUI text in scoreText) text.text = "0";
        walkSpeed = initialWalkSpeed;
        //onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Rigidbody2D rb = horizonFlipped ? rbYang : rbYin;
        cameraPosition.x = rb.position.x + 5;
        cameraPosition.y = rb.position.y;
        Camera.main.transform.position = cameraPosition;
    }

    void FixedUpdate()
    {
        if (gameOver) return;
        Rigidbody2D rb = horizonFlipped ? rbYang : rbYin;
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
                rbYin.gravityScale *= -1;
                rbYang.gravityScale *= -1;
                if (horizonFlipped) rbYang.MovePosition(rbYin.position - Vector2.up * 0.86f);
                else rbYin.MovePosition(rbYang.position + Vector2.up * 0.86f);
                animator.PlayWalkAnimation(!horizonFlipped);
                walkSpeed += walkSpeedIncrementPerSecond * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2();
                rb.AddForce(Vector2.up * jumpForce * (horizonFlipped ? -1 : 1), ForceMode2D.Impulse);
                animator.PlayJumpAnimation(!horizonFlipped);
            }
            //else rb.MovePosition(rb.position + Vector2.right * walkSpeed * Time.deltaTime);
        }
        //else rb.MovePosition(rb.position + Vector2.right * walkSpeed * Time.deltaTime);
        score += walkSpeed * Time.deltaTime;
        foreach (TextMeshProUGUI text in scoreText) text.text = (int)score + "";
        //if (Mathf.Abs(walkInput) > 0.1f) rigidBody.AddForce(Vector2.right * walkInput * walkSpeed, ForceMode2D.Impulse);
        if (horizonFlipped ? rb.velocity.y > 0 : rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
            animator.PlayFallAnimation(!horizonFlipped);
        }
        else if (horizonFlipped ? rb.velocity.y < 0 : rb.velocity.y > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
    }

    public void CollisionEnter(GameObject collider)
    {
        if (collider == (horizonFlipped ? colliderYang : colliderYin))
        {
            iconYang.SetActive(false);
            iconYin.SetActive(false);
            gameOver = true;
        }
        else if (!onGround && collider == (horizonFlipped ? playerYang : playerYin))
        {
            onGround = true;
            animator.PlayWalkAnimation(!horizonFlipped);
        }
    }

    public void CollisionExit(GameObject collider)
    {
        if (onGround && onGround && collider == (horizonFlipped ? playerYang : playerYin)) onGround = false;
    }
}
