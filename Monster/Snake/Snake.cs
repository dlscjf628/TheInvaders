using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeState snakeState;

    GameObject player;
    public GameObject stunBall;

    float t1, t;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance >= 5.0f)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * snakeState.speed * Time.deltaTime);
        }
        t += Time.deltaTime;
        if (t > 3f)
        {
            t = 0;
            Instantiate(stunBall, transform.position, Quaternion.identity);
        }
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
                target.OnDamage(snakeState.damage, 1);
            }
        }
    }
}
