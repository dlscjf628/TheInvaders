using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBall : MonoBehaviour
{
    public float damage = 2f;
    public float speed = 6f;
    GameObject player;
    float monsterX, monsterY, monsterZ;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
        direction = (player.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime);
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(monsterX, monsterY, monsterZ);
        }
        else
        {
            transform.localScale = new Vector3(-monsterX, monsterY, monsterZ);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Damage target = collision.GetComponent<Damage>();
            target.OnDamage(damage, 4);
            Destroy(gameObject);
        }
        else if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
