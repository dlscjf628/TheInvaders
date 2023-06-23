using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.localPosition.x;
        y = transform.localPosition.y;
        z = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(x, y, z);
    }
}
