using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerITmancik : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;

    public GameObject FloorUp;
    public GameObject FloorDown;

    private int IndexOfFloor = 1;

    public float JumpPower = 3.0f;

    public int ClickToChangeFloor = 2;
    private int ClickedToChangeFloor;

    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius = 0.1f;

    public bool CanSwitchFloor;

    public bool ActiveUpFloor;
    public bool ActiveDownFloor;

    private bool CanSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(ActiveUpFloor && CanSwitch)
            {
                if (onGround)
                {
                    //SwicthUpFloor();
                    Debug.Log("Switched Up");
                    //IndexOfFloor = 2;
                    ActiveDownFloor = true;
                    ActiveUpFloor = false;
                    this.transform.position = FloorDown.transform.position;
                    this.transform.localScale = new Vector3(1, -1, 1);
                    rb.gravityScale = -1;
                    CanSwitch = false;
                    StartCoroutine(SwitchTimeReload());
                    //this.transform.position = FloorDown.transform.position;
                    //this.transform.localScale = new Vector3(1, 1, 1);
                    //rb.gravityScale = 1;
                }
            }

            if (ActiveDownFloor && CanSwitch)
            {
                if (onGround)
                {
                    //SwicthDownFloor();
                    Debug.Log("Switched Down");
                    //IndexOfFloor = 1;
                    ActiveUpFloor = true;
                    ActiveDownFloor = false;
                    this.transform.position = FloorUp.transform.position;
                    this.transform.localScale = new Vector3(1, 1, 1);
                    rb.gravityScale = 1;
                    CanSwitch = false;
                    StartCoroutine(SwitchTimeReload());
                    //this.transform.position = FloorUp.transform.position;
                    //rb.gravityScale = 1;
                    //this.transform.localScale = new Vector3(1, -1, 1);
                }
            }
        }

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        CheckingGround();
    }

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
    }

    void Jump()
    {
        if (ActiveUpFloor) rb.velocity = transform.up * JumpPower;

        if (ActiveDownFloor) rb.velocity = transform.up * -JumpPower;
    }


    IEnumerator SwitchTimeReload()
    {
        CanSwitch = false;
        yield return new WaitForSeconds(1f);
        CanSwitch = true;
    }

}
