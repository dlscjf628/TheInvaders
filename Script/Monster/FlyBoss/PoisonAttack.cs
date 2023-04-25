using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAttack : MonoBehaviour
{

    Animator ani;

    Vector2 randomDirection;

    float speed=0.01f;
    float t, randT;
    float t2 = 1f;
    float damage = 2f;
    
    private void Start()
    {
        ani = GetComponent<Animator>();
        randomDirection = Random.insideUnitCircle.normalized;
        randT = Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t < randT)
        {
            transform.Translate(randomDirection * speed);
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            ani.SetTrigger("Attack");
            if (t > 5)
                ani.SetTrigger("End");
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t2 += Time.deltaTime;
            if (t2 > 0.5f)
            {
                t2 = 0;
                Damage target = collision.GetComponent<Damage>();
                target.OnDamage(damage, 1);
            }
        }
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
