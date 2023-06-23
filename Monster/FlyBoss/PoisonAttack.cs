using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAttack : MonoBehaviour
{
    GameObject flyBoss;
    Animator ani;

    Vector2 randomDirection;

    float speed=0.01f;
    float t, randT;
    float t2 = 1f;
    public float damage = 5f;
    
    private void Start()
    {
        flyBoss = GameObject.Find("FlyBoss&Hp(Clone)");
        ani = GetComponent<Animator>();
        randomDirection = Random.insideUnitCircle.normalized;
        randT = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if (flyBoss.transform.GetChild(0).gameObject.GetComponent<FlyBossState>().hp <= 0)
            Destroy(gameObject);

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
                target.OnDamage(damage, 3);
            }
        }
        else if (collision.tag == "Wall")
        {
            speed = 0f;
        }
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
