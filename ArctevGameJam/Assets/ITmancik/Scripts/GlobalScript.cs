using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static int pointsScore;
    public static int carrotsScore;

    public TextMeshProUGUI pointsScoreText;

    public TextMeshProUGUI carrotScoreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddScore());
    }

    // Update is called once per frame
    void Update()
    {
        pointsScoreText.text = $"Score:{pointsScore}";
        carrotScoreText.text = $"Carrots:{carrotsScore}";
    }

    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(0.02f);
        pointsScore++;
        StartCoroutine(AddScore());
    }
}
