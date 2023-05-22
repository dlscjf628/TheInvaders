using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBuff : MonoBehaviour
{
    public Text buffText;
    GameObject player;

    int stage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (stage != Manager.instance.level)
        {
            stage = Manager.instance.level;
            int n = Random.Range(0, 2);

            if (n == 0)
            {
                MoveMentSpeedIncrease();
            }
            else if (n == 1)
            {
                MoveMentSpeedreduction();
            }
        }   
    }

    void MoveMentSpeedIncrease()
    {
        player.GetComponent<bl_ControllerExample>().Speed *= 1.2f;
        buffText.text = "이동속도가 20%증가";
    }

    void MoveMentSpeedreduction()
    {
        player.GetComponent<bl_ControllerExample>().Speed *= 0.8f;
        buffText.text = "이동속도가 20%감소";
    }
}
