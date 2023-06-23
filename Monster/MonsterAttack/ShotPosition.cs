using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPosition : MonoBehaviour
{
    public GameObject ball;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 1.5f)
        {
            Instantiate(ball, transform.position, Quaternion.identity);
            t = 0;
        }
    }
}
