using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
               // damageable.OnDamage(10f);
            }
        }
    }

    private float CalculateDamage()
    {
        float baseDamage = 10f;
        float damageMultiplier = 1.0f;

        float modifiedDamage = baseDamage * damageMultiplier; // 데미지 계산
        return modifiedDamage;
    }
}
