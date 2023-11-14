using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxMove : MonoBehaviour
{
    public float parralaxSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(parralaxSpeed, 0, 0);
    }
}
