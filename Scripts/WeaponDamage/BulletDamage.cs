using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage;
    public bool Holy;
    public bool MagicAndMelee;

    public InforMation info;
    void OnEnable()
    {
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        if (!MagicAndMelee)
        {
            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            
            if (!MagicAndMelee)
            {
                Destroy(gameObject);
            }
            if (Holy)
            {
                if (info.PlayerHP < info.PlayerMaxHP)
                {
                    info.PlayerHP += 1f;
                }
            }
            
            Damage target = collision.GetComponent<Damage>();
            if (target != null)
            {
                target.OnDamage(damage, 1);
            }

        }
        else if (collision.tag == "Wall" && !MagicAndMelee)
        {
            Destroy(gameObject);
        }
    }
}
