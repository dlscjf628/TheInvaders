using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMonster : MonoBehaviour
{
    [SerializeField] private PoisonState poisonState;

    GameObject player;

    float t1;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * poisonState.speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t1 += Time.deltaTime;
            if (t1 > 0.5f)
            {
                t1 = 0;
                Damage target = collision.GetComponent<Damage>();
                target.OnDamage(poisonState.damage, 3);
            }
        }
    }

}
