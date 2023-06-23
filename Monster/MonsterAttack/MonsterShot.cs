using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShot : MonoBehaviour
{
    Damage target;
    
    GameObject player;
    
    public float speed = 4f;
    public float damage;

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
            target.OnDamage(damage, 1);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
