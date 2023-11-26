using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [SerializeField] private AudioSource bGM;
    [SerializeField] private AudioSource sFX;

    // Start is called before the first frame update
    void Start()
    {
        bGM.Play();
        sFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bGM.isPlaying)
        {
            bGM.Play();
            sFX.Play();
        }
    }
}
