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
    [SerializeField] private GameObject[] gameOverScreenFrames;

    [SerializeField] private PlayerAnimation animatorYin;
    [SerializeField] private PlayerAnimation animatorYang;

    [SerializeField] private TimeManager timeManager;
    [SerializeField] private RoomGenerator generator;

    [SerializeField] private AudioSource musicYang;
    [SerializeField] private AudioSource musicYin;

    [SerializeField] private AudioSource GameOverSound;
    [SerializeField] private AudioSource JumpSound;
    [SerializeField] private AudioSource FlipHorizonSound;
    [SerializeField] private AudioSource PowerupExpireSound;

    [SerializeField] private float cameraOffsetX;
    [SerializeField] private float jumpForce;
    [SerializeField] private float powerupJumpForce;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;

    [SerializeField] private float triggerCountdown;

    [SerializeField] private bool mirror;

    private Rigidbody2D rbYin;
    private Rigidbody2D rbYang;
    private bool onGround;
    private bool falling;
    private bool horizonFlipped;
    private bool jumpPowerup;
    private float triggerTime;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        if (mirror) Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        playerYang.SetActive(false);
        backgroundYin.SetActive(false);
        textYin.SetActive(false);
        iconYin.SetActive(false);
        gameOverScreen.SetActive(false);
        animatorYin.PlayWalkAnimation();
        animatorYang.PlayWalkAnimation();
        musicYin.volume = 0;
        musicYin.Play();
        musicYang.Play();
        rbYin = playerYin.GetComponent<Rigidbody2D>();
        rbYang = playerYang.GetComponent<Rigidbody2D>();
        generator.SetYin(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!((horizonFlipped ? musicYin : musicYang).isPlaying))
        {
            musicYin.Play();
            musicYang.Play();
        }
        Vector3 cameraPosition = Camera.main.transform.position;
        Rigidbody2D rb = horizonFlipped ? rbYang : rbYin;
        cameraPosition.x = rb.position.x + cameraOffsetX;
        cameraPosition.y = rb.position.y;
        Camera.main.transform.position = cameraPosition;
        if (triggerTime > 0)
        {
            triggerTime -= Time.deltaTime;
            if (triggerTime <= 0) SetGameOver();
        }

        CodeFromFixedUpdate();
    }

    void FixedUpdate()
    {
        /*
        if (gameOver) return;
        Rigidbody2D rb = horizonFlipped ? rbYang : rbYin;
        if (onGround)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(1, -1, 1));
                horizonFlipped = !horizonFlipped;
                generator.HideAll();
                (horizonFlipped ? playerYang : playerYin).SetActive(true);
                backgroundYin.SetActive(horizonFlipped);
                backgroundYang.SetActive(!horizonFlipped);
                textYin.SetActive(horizonFlipped);
                textYang.SetActive(!horizonFlipped);
                iconYin.SetActive(horizonFlipped);
                iconYang.SetActive(!horizonFlipped);
                rbYin.gravityScale *= -1;
                rbYang.gravityScale *= -1;
                if (horizonFlipped) rbYang.MovePosition(rbYin.position - Vector2.up * 1.22f);
                else rbYin.MovePosition(rbYang.position + Vector2.up * 1.22f);
                (horizonFlipped ? playerYin : playerYang).SetActive(false);
                generator.SetYin(!horizonFlipped);
                musicYang.volume = horizonFlipped ? 0 : 1;
                musicYin.volume = horizonFlipped ? 1 : 0;
                FlipHorizonSound.Play();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2();
                rb.AddForce(Vector2.up * (jumpPowerup ? powerupJumpForce : jumpForce) * (horizonFlipped ? -1 : 1), ForceMode2D.Impulse);
                JumpSound.Play();
                animatorYin.PlayJumpAnimation();
                animatorYang.PlayJumpAnimation();
            }
        }
        if (!falling && (horizonFlipped ? rb.velocity.y > 0 : rb.velocity.y < 0))
        {
            onGround = false;
            falling = true;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
            animatorYin.SetFallSprite();
            animatorYang.SetFallSprite();
        }
        else if (horizonFlipped ? rb.velocity.y < 0 : rb.velocity.y > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
        */
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
            if (other.tag == "Platform" || other.tag == "Hazard") triggerTime = triggerCountdown;
        }
    }

    public void CollisionExit(GameObject collider, GameObject other)
    {
        if (collider == (horizonFlipped ? playerYang : playerYin) && onGround && other.tag == "Platform") onGround = false;
        else if (collider == (horizonFlipped ? colliderYang : colliderYin) && triggerTime > 0) triggerTime = 0;
    }
    
    public void GetJumpPowerup(float duration)
    {
        StartCoroutine(JumpPowerup(duration));
    }

    private IEnumerator JumpPowerup(float duration)
    {
        jumpPowerup = true;
        yield return new WaitForSeconds(duration);
        jumpPowerup = false;
        PowerupExpireSound.Play();
    }

    public void GetReversePowerup(float duration)
    {
        StartCoroutine(ReversePowerup(duration));
    }

    private IEnumerator ReversePowerup(float duration)
    {
        Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        yield return new WaitForSeconds(duration);
        Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        PowerupExpireSound.Play();
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    private void SetGameOver()
    {
        playerYin.SetActive(false);
        playerYin.SetActive(false);
        iconYang.SetActive(false);
        iconYin.SetActive(false);
        gameOverScreen.SetActive(true);
        GameOverSound.Play();
        gameOver = true;
        StartCoroutine(AnimateGameOverScreen());
    }

    private IEnumerator AnimateGameOverScreen()
    {
        int frame = 1;
        bool forward = true;
        do
        {
            for (int i = 0; i < gameOverScreenFrames.Length; i++) gameOverScreenFrames[i].SetActive(i == frame);
            yield return new WaitForSeconds(0.08112f);
            if (frame == 1) forward = true;
            else if (frame == gameOverScreenFrames.Length - 1) forward = false;
            if (forward) frame++;
            else frame--;
        }
        while (frame < gameOverScreenFrames.Length);
    }

    void CodeFromFixedUpdate()
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
                (horizonFlipped ? playerYang : playerYin).SetActive(true);
                backgroundYin.SetActive(horizonFlipped);
                backgroundYang.SetActive(!horizonFlipped);
                textYin.SetActive(horizonFlipped);
                textYang.SetActive(!horizonFlipped);
                iconYin.SetActive(horizonFlipped);
                iconYang.SetActive(!horizonFlipped);
                rbYin.gravityScale *= -1;
                rbYang.gravityScale *= -1;
                if (horizonFlipped) rbYang.MovePosition(rbYin.position - Vector2.up * 1.22f);
                else rbYin.MovePosition(rbYang.position + Vector2.up * 1.22f);
                (horizonFlipped ? playerYin : playerYang).SetActive(false);
                generator.SetYin(!horizonFlipped);
                musicYang.volume = horizonFlipped ? 0 : 1;
                musicYin.volume = horizonFlipped ? 1 : 0;
                FlipHorizonSound.Play();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2();
                rb.AddForce(Vector2.up * (jumpPowerup ? powerupJumpForce : jumpForce) * (horizonFlipped ? -1 : 1), ForceMode2D.Impulse);
                JumpSound.Play();
                animatorYin.PlayJumpAnimation();
                animatorYang.PlayJumpAnimation();
            }
        }
        if (!falling && (horizonFlipped ? rb.velocity.y > 0 : rb.velocity.y < 0))
        {
            onGround = false;
            falling = true;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
            animatorYin.SetFallSprite();
            animatorYang.SetFallSprite();
        }
        else if (horizonFlipped ? rb.velocity.y < 0 : rb.velocity.y > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * (horizonFlipped ? -1 : 1) * Time.deltaTime;
    }
}
