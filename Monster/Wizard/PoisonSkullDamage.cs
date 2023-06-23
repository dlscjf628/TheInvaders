using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSkullDamage : MonoBehaviour
{
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Damage target = collision.GetComponent<Damage>();
            target.OnDamage(damage, 3);
            Destroy(transform.parent.gameObject);
        }
    }
}
