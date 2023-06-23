using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour
{
    float damage = 5f;
    GameObject evil;

    private void Start()
    {
        evil = GameObject.Find("EvilGod(Clone)");
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        if (evil.transform.GetChild(0).gameObject.GetComponent<EvilGodState>().hp <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Damage target = collision.GetComponent<Damage>();
            target.OnDamage(damage, 1);
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
