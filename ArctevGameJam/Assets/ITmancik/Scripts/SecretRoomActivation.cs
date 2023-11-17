using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretRoomActivation : MonoBehaviour
{
    int nextMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && nextMove == 0) nextMove++;
        if (Input.GetKeyDown(KeyCode.A) && nextMove == 1) nextMove++;
        if (Input.GetKeyDown(KeyCode.B) && nextMove == 2) nextMove++;
        if (Input.GetKeyDown(KeyCode.B) && nextMove == 3) nextMove++;
        if (Input.GetKeyDown(KeyCode.I) && nextMove == 4) nextMove++;
        if (Input.GetKeyDown(KeyCode.T) && nextMove == 5)
        {
            SceneManager.LoadScene("SecretScene");
            nextMove++;
        }
    }
}
