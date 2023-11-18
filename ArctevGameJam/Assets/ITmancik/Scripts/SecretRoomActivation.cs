using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretRoomActivation : MonoBehaviour
{
    int nextMove;
    private int giantIndex;
    private int mirrorIndex;
    private int moonwalkIndex;
    private int carrotIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (giantIndex == 0) giantIndex++;
            else giantIndex = 0;
            mirrorIndex = 0;
            moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (giantIndex == 1) giantIndex++;
            else giantIndex = 0;
            if (mirrorIndex == 1) mirrorIndex++;
            else mirrorIndex = 0;
            moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (giantIndex == 2) giantIndex++;
            else giantIndex = 0;
            mirrorIndex = 0;
            if (moonwalkIndex == 5) moonwalkIndex++;
            else moonwalkIndex = 0;
            if (carrotIndex == 1) carrotIndex++;
            else carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (giantIndex == 3) giantIndex++;
            else giantIndex = 0;
            mirrorIndex = 0;
            if (moonwalkIndex == 3) moonwalkIndex++;
            else moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (giantIndex == 4) SceneManager.LoadScene("GiantEasterEggScene");
            else giantIndex = 0;
            mirrorIndex = 0;
            moonwalkIndex = 0;
            if (carrotIndex == 5) SceneManager.LoadScene("CarrotEasterEggScene");
            else carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            giantIndex = 0;
            if (mirrorIndex == 0) mirrorIndex++;
            else mirrorIndex = 0;
            if (moonwalkIndex == 0) moonwalkIndex++;
            else moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            giantIndex = 0;
            if (mirrorIndex == 2 || mirrorIndex == 3) mirrorIndex++;
            else if (mirrorIndex == 5) SceneManager.LoadScene("MirrorEasterEggScene");
            else mirrorIndex = 0;
            moonwalkIndex = 0;
            if (carrotIndex == 2 || carrotIndex == 3) carrotIndex++;
            else carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            giantIndex = 0;
            if (mirrorIndex == 4) mirrorIndex++;
            else mirrorIndex = 0;
            if (moonwalkIndex == 1 || moonwalkIndex == 2) moonwalkIndex++;
            else moonwalkIndex = 0;
            if (carrotIndex == 4) carrotIndex++;
            else carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            giantIndex = 0;
            mirrorIndex = 0;
            if (moonwalkIndex == 4) moonwalkIndex++;
            else moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            giantIndex = 0;
            mirrorIndex = 0;
            if (moonwalkIndex == 6) moonwalkIndex++;
            else moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            giantIndex = 0;
            mirrorIndex = 0;
            if (moonwalkIndex == 7) SceneManager.LoadScene("MoonwalkEasterEggScene");
            else moonwalkIndex = 0;
            carrotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            giantIndex = 0;
            mirrorIndex = 0;
            moonwalkIndex = 0;
            if (carrotIndex == 0) carrotIndex++;
            else carrotIndex = 0;
        }
        //Debug.Log(mirrorIndex);

        /*
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
        */
    }
}
