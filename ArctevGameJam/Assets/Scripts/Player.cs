using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerYin;
    [SerializeField] private GameObject playerYang;
    [SerializeField] private GameObject colliderYin;
    [SerializeField] private GameObject colliderYang;
    [SerializeField] private GameObject backgroundYin;
    [SerializeField] private GameObject backgroundYang;
    [SerializeField] private GameObject textYin;
    [SerializeField] private GameObject textYang;
    [SerializeField] private GameObject iconYin;
    [SerializeField] private GameObject iconYang;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private PlayerAnimation animatorYin;
    [SerializeField] private PlayerAnimation animatorYang;

    [SerializeField] private RoomGenerator generator;

    [SerializeField] private AudioSource musicYang;
    [SerializeField] private AudioSource musicYin;
    [SerializeField] private AudioSource musicGameOver;

    [SerializeField] private float cameraOffsetX;
    public float jumpForce;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;

    private Rigidbody2D rbYin;
    private Rigidbody2D rbYang;
    private bool onGround;
    private bool falling;
    private bool horizonFlipped;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerYang.SetActive(false);
        backgroundYin.SetActive(false);
        textYin.SetActive(false);
        iconYin.SetActive(false);
        gameOverScreen.SetActive(false);
        musicYin.volume = 0;
        musicGameOver.volume = 0;
        rbYin = playerYin.GetComponent<Rigidbody2D>();
        rbYang = playerYang.GetComponent<Rigidbody2D>();
        generator.SetYin(true);
        //onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Rigidbody2D rb = horizonFlipped ? rbYang : rbYin;
        cameraPosition.x = rb.position.x + cameraOffsetX;
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
                Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(1, -1, 1));
                horizonFlipped = !horizonFlipped;
                generator.HideAll();
                playerYin.SetActive(!horizonFlipped);
                playerYang.SetActive(horizonFlipped);
                backgroundYin.SetActive(horizonFlipped);
                backgroundYang.SetActive(!horizonFlipped);
                textYin.SetActive(horizonFlipped);
                textYang.SetActive(!horizonFlipped);
                iconYin.SetActive(horizonFlipped);
                iconYang.SetActive(!horizonFlipped);
                rbYin.gravityScale *= -1;
                rbYang.gravityScale *= -1;
                if (horizonFlipped) rbYang.MovePosition(rbYin.position - Vector2.up * 0.9f);
                else rbYin.MovePosition(rbYang.position + Vector2.up * 0.9f);
                generator.SetYin(!horizonFlipped);
                animatorYin.PlayWalkAnimation();
                animatorYang.PlayWalkAnimation();
                musicYang.volume = horizonFlipped ? 0 : 1;
                musicYin.volume = horizonFlipped ? 1 : 0;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2();
                rb.AddForce(Vector2.up * jumpForce * (horizonFlipped ? -1 : 1), ForceMode2D.Impulse);
                (horizonFlipped ? animatorYang : animatorYin).PlayJumpAnimation();
                animatorYin.PlayJumpAnimation();
                animatorYang.PlayJumpAnimation();
            }
            //else rb.MovePosition(rb.position + Vector2.right * walkSpeed * Time.deltaTime);
        }
        //else rb.MovePosition(rb.position + Vector2.right * walkSpeed * Time.deltaTime);
        //if (Mathf.Abs(walkInput) > 0.1f) rigidBody.AddForce(Vector2.right * walkInput * walkSpeed, ForceMode2D.Impulse);
        if (!falling && (horizonFlipped ? rb.velocity.y > 0 : rb.velocity.y < 0))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
            (horizonFlipped ? animatorYang : animatorYin).SetFallSprite();
            animatorYin.SetFallSprite();
            animatorYang.SetFallSprite();
            falling = true;
        }
        else if (horizonFlipped ? rb.velocity.y < 0 : rb.velocity.y > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
    }

    public void SetAnimationSpeedFactor(float factor)
    {
        animatorYin.SetAnimationSpeedFactor(factor);
        animatorYang.SetAnimationSpeedFactor(factor);
    }

    public void CollisionEnter(GameObject collider, GameObject other)
    {
        if (collider == (horizonFlipped ? playerYang : playerYin))
        {
            if (other.tag == "Hazard") SetGameOver();
            else if (!onGround && other.tag == "Platform")
            {
                onGround = true;
                falling = false;
                animatorYin.PlayWalkAnimation();
                animatorYang.PlayWalkAnimation();
            }
        }
        else if (collider == (horizonFlipped ? colliderYang : colliderYin))
        {
            if (other.tag == "Platform" || other.tag == "Hazard") SetGameOver();
        }
    }

    public void CollisionExit(GameObject collider, GameObject other)
    {
        if (collider == (horizonFlipped ? playerYang : playerYin) && onGround && other.tag == "Platform") onGround = false;
    }
    
    public bool GetGameOver()
    {
        return gameOver;
    }

    private void SetGameOver()
    {
        iconYang.SetActive(false);
        iconYin.SetActive(false);
        gameOverScreen.SetActive(true);
        musicYang.volume = 0;
        musicYin.volume = 0;
        musicGameOver.volume = 1;
        gameOver = true;
    }
}
