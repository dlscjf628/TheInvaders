using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowShot : MonoBehaviour
{
    Damage target;

    GameObject player;

    float speed = 8f;
    float damage = 2f;

    Vector2 v;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        v = (player.transform.position - transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(v * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = collision.GetComponent<Damage>();
            target.OnDamage(damage, 2);
            Destroy(gameObject);
        }
    }
}
