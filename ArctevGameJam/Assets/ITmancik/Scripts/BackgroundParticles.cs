using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ParticleSystem>() != null)
        {
            Debug.Log("ParticleTrigger");
            if (collision.tag == "WhiteParticle")
            {
                collision.GetComponent<ParticleSystem>().startColor = Color.black;
                Debug.Log("ParticleTriggerWhite");
            }

            if (collision.CompareTag("DarkParticle")) 
            { 
                collision.GetComponent<ParticleSystem>().startColor = Color.white;
                Debug.Log("ParticleTriggerDark");
            }
        }
    }
    
    void OnParticleTrigger()
    {
        Debug.Log("ParticleTrigger");
    }
    
}
