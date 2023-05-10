using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamage : MonoBehaviour
{
    float damage = 2f;

    private void Start()
    {
        Destroy(gameObject, 3f);
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
