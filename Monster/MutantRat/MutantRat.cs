using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantRat : MonoBehaviour, Damage
{
    MutantState mState;

    GameObject player;

    Rigidbody2D rigid;
    Collider2D coll;

    bool isLive;
    float t = 1f;
    float monsterX, monsterY, monsterZ;
    // Start is called before the first frame update
    void Start()
    {
        mState = GetComponent<MutantState>();
        player = GameObject.FindGameObjectWithTag("Player");

        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * mState.speed * Time.deltaTime);
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(monsterX), monsterY, monsterZ);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(monsterX), monsterY, monsterZ);
        }
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
            t += Time.deltaTime;
            if (t > 0.5f)
            {
                t = 0;
                Damage target = collision.GetComponent<Damage>();
                target.OnDamage(mState.damage, 1);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t = 1.0f;
        }
    }

    void OnEnable()
    {
        mState = GetComponent<MutantState>();
        player = GameObject.FindGameObjectWithTag("Player");

        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;

        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
        transform.localScale = new Vector3(Mathf.Abs(monsterX), monsterY, monsterZ);
    }

}
