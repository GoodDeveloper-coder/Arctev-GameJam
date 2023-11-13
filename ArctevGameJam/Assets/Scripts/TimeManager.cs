using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private RoomGenerator generator;
    [SerializeField] private TextMeshProUGUI[] scoreText;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncrementPerSecond;

    private PlayerAnimation animator;

    private float currentSpeed;
    private float score;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        animator = player.gameObject.GetComponent<PlayerAnimation>();
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        if (player.GetGameOver())
        {
            stop = true;
            return;
        }
        score += currentSpeed * Time.deltaTime;
        currentSpeed += speedIncrementPerSecond * Time.deltaTime;
        animator.SetAnimationSpeedFactor(currentSpeed / initialSpeed);
        foreach (TextMeshProUGUI text in scoreText) text.text = (int)score + "";
    }
}
