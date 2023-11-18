using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    private Player player;
    private AudioSource CollectSound;
    private RoomGenerator generator;
    private TimeManager timeManager;

    private int powerupType;

    [SerializeField] private Sprite[] powerupSprites;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float powerupDuration;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        CollectSound = GameObject.Find("CollectPowerUpSound").GetComponent<AudioSource>();
        generator = GameObject.Find("Room Generator").GetComponent<RoomGenerator>();
        timeManager = GameObject.Find("Time Manager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            switch (powerupType)
            {
                case 0:
                    player.GetJumpPowerup(powerupDuration);
                    break;
                case 1:
                    player.GetReversePowerup(powerupDuration);
                    break;
                case 2:
                    player.GetSlowPowerup(powerupDuration);
                    generator.GetSlowPowerup(powerupDuration);
                    break;
                case 3:
                    timeManager.GetMultiplierPowerup(powerupDuration);
                    break;
            }
            CollectSound.Play();
            Destroy(this.gameObject);
        }
    }

    public void SetPowerupType(int type)
    {
        powerupType = type;
        spriteRenderer.sprite = powerupSprites[type];
    }
}
