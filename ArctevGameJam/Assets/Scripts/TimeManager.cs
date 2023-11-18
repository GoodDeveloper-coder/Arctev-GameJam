using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private RoomGenerator generator;
    [SerializeField] private TextMeshProUGUI[] scoreText;

    [SerializeField] private AudioSource PowerupExpireSound;

    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private Vector2 particleBoundaries;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncrementPerSecond;

    [SerializeField] private float initialParticleFrequency;

    private List<GameObject> particles;
    private List<Vector3> particleTargets;
    private List<bool> particleClockwise;
    private bool particleSpawning;

    private float currentSpeed;
    private float score;
    private bool multiplierPowerup;
    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        particles = new List<GameObject>();
        particleTargets = new List<Vector3>();
        particleClockwise = new List<bool>();
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
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
        
        if (!particleSpawning) StartCoroutine(SpawnParticles());
        List<int> particlesToRemove = new List<int>();
        for (int i = 0; i < particles.Count; i++)
        {
            GameObject p = particles[i];
            if (Mathf.Abs(p.transform.position.x) > Mathf.Abs(particleBoundaries.x) || Mathf.Abs(p.transform.position.y) > Mathf.Abs(particleBoundaries.y)) particlesToRemove.Add(i);
            else
            {
                p.transform.position = Vector3.MoveTowards(p.transform.position, particleTargets[i], currentSpeed * Time.deltaTime);
                p.transform.Rotate(0, 0, currentSpeed * (particleClockwise[i] ? -30 : 30) * Time.deltaTime, Space.World);
            }
        }
        for (int i = 0; i < particlesToRemove.Count; i++)
        {
            Destroy(particles[particlesToRemove[i]]);
            particles.RemoveAt(particlesToRemove[i]);
            particleTargets.RemoveAt(particlesToRemove[i]);
            particleClockwise.RemoveAt(particlesToRemove[i]);
        }
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
        multiplierPowerup = false;
        PowerupExpireSound.Play();
    }

    private IEnumerator SpawnParticles()
    {
        particleSpawning = true;
        yield return new WaitForSeconds(initialSpeed / (initialParticleFrequency * currentSpeed));
        particles.Add(Instantiate(particlePrefab, new Vector3(particleBoundaries.x, Random.Range(-particleBoundaries.y, particleBoundaries.y), 0), transform.rotation));
        particleTargets.Add(new Vector3(-particleBoundaries.x, Random.Range(-particleBoundaries.y, particleBoundaries.y), 0));
        particleClockwise.Add(Random.Range(0, 2) == 0);
        particleSpawning = false;
    }
}
