using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTimer : MonoBehaviour
{
    public Text countdownText;
    float countdownValue = 60f;

    void Start()
    {
        InvokeRepeating("UpdateCountdown", 0f, 0.01f);
    }

    void UpdateCountdown()
    {
        countdownValue -= 0.01f;
        if (countdownValue <= 0)
        {
            countdownValue = 0;
        }
        countdownText.text = countdownValue.ToString("F2");

        if (countdownValue <= 0f)
        {
            
        }
    }
}