using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float baseDamage = 10f;
    public float damageCoefficient = 1.0f;
    float MonsterHP;/*
    public void DealDamage()
    {
        float calculatedDamage = baseDamage * damageCoefficient;
        //OnDamage(calculatedDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<MonsterState>().hp <= 0)
            {
                Destroy(collision.gameObject);
            }
            MonsterHP = collision.gameObject.GetComponent<MonsterState>().hp;
            DealDamage();
            collision.gameObject.GetComponent<MonsterState>().hp = MonsterHP;
        }
    }/*
    public virtual void OnDamage(float damage)
    {
        // 데미지 처리 로직
        MonsterHP -= damage;
        Debug.Log("준 데미지: " + damage);
        print("남은 hp : " + MonsterHP);
    }*/
}
