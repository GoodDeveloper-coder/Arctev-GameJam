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

    private float currentSpeed;
    private float score;
    private bool multiplierPowerup;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        if (player.GetGameOver())
        {
            stop = true;
            generator.SetSpeed(0);
            return;
        }
        score += currentSpeed * Time.deltaTime;
        currentSpeed += speedIncrementPerSecond * Time.deltaTime;
        generator.SetSpeed(currentSpeed);
        player.SetAnimationSpeedFactor(currentSpeed / initialSpeed);
        foreach (TextMeshProUGUI text in scoreText) text.text = (int)score + "";
    }

    public void AddScore(float points)
    {
        score += (multiplierPowerup ? points * 2 : points);
        foreach (TextMeshProUGUI text in scoreText) text.text = (int)score + "";
    }

    public void GetMultiplierPowerup(float duration)
    {
        StartCoroutine(MultiplierPowerup(duration));
    }

    private IEnumerator MultiplierPowerup(float duration)
    {
        multiplierPowerup = true;
        yield return new WaitForSeconds(duration);
        Camera.main.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        multiplierPowerup = false;
    }
}
