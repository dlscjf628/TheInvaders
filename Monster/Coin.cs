using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player;
    GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (manager.GetComponent<InforMation>().PlayerJob == 3) manager.GetComponent<InforMation>().Coin++;
            manager.GetComponent<InforMation>().Coin++;
            gameObject.SetActive(false);
        }
    }
}
