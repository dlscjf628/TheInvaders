using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMonster : MonoBehaviour, Damage
{
    [SerializeField] PoisonState mState;

    GameObject player;

    Rigidbody2D rigid;
    Collider2D coll;

    bool isLive;

    float t1 = 1f;

    void Awake()
    {
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * mState.speed * Time.deltaTime);
    }

    public virtual void OnDamage(float damgage, int mode)
    {
        mState.hp -= damgage;
        if (mState.hp <= 0)
        {
            isLive = false;
            rigid.simulated = false;
            coll.enabled = false;
            GameObject coin = Manager.instance.pool.GetCoin();
            coin.transform.position = transform.position;
            gameObject.SetActive(false);
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
                target.OnDamage(mState.damage, 3);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t1 = 1.0f;
        }
    }

    void OnEnable()
    {
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;


    }

}
