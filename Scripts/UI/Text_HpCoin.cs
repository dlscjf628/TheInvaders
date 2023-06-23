using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Text_HpCoin : MonoBehaviour
{
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI CoinText;
    public InforMation info;

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        HpText = GameObject.FindWithTag("HPText").GetComponent<TextMeshProUGUI>();
        CoinText = GameObject.FindWithTag("CoinCount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HpText.text = info.PlayerHP.ToString("F0")+ " / " + info.PlayerMaxHP.ToString("F0");
        CoinText.text = info.Coin.ToString();
    }
}
