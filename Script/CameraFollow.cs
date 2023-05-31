using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;

    float minX = -16f;
    float maxX = 16f;
    float minY = -9f;
    float maxY = 9f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float clampX = Mathf.Clamp(player.transform.position.x, minX, maxX);
        float clampY = Mathf.Clamp(player.transform.position.y, minY, maxY);
        transform.position = new Vector3(clampX, clampY, -10f);

    }
}
