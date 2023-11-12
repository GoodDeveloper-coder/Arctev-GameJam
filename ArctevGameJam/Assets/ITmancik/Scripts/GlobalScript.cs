using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static int score;

    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddScore());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score:{score}";
    }

    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(0.02f);
        score++;
        StartCoroutine(AddScore());
    }
}
