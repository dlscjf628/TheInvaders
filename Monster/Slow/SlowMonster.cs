using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMonster : MonoBehaviour, Damage
{
    SlowState mState;

    Rigidbody2D rigid;
    Collider2D coll;

    bool isLive;

    float t = 1f;

    //public RuntimeAnimatorController[] animCon;

    GameObject player;
    Spawner spawn;
    //Animator ani;
    float a;
    float monsterX, monsterY, monsterZ;
    bool attackBack;
    // Start is called before the first frame update
    void Awake()
    {
        mState = GetComponent<SlowState>();
        //ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.gameOver && !attackBack && isLive)
        {
            Move();
            rigid.velocity = Vector2.zero;
        }
        else if (attackBack)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 0.1f, ForceMode2D.Impulse);

        }
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance >= 5.0f)
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
    }

    public virtual void OnDamage(float damgage, int mode)
    {
        GameObject effect = Manager.instance.pool.Geteffect();
        effect.transform.position = new Vector2(transform.position.x, transform.position.y);

        mState.hp -= damgage;
        if (!attackBack)
        {
            StartCoroutine(Back());
        }
        if (mState.hp <= 0)
        {
            isLive = false;
            rigid.simulated = false;
            coll.enabled = false;
            int n = Random.Range(0, 5);
            if (n < 3)
            {
                GameObject enermy = Manager.instance.pool.GetCoin();
                enermy.transform.position = new Vector2(transform.position.x, transform.position.y);
            }
            spawn.cnt--;
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

    IEnumerator Back()
    {
        attackBack = true;
        yield return new WaitForSeconds(0.1f);
        attackBack = false;

    }
    void OnEnable()
    {
        mState = GetComponent<SlowState>();
        //ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spawn = GameObject.Find("PoolManager").gameObject.transform.GetChild(0).gameObject.GetComponent<Spawner>();
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;

        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;

        rigid.velocity = Vector2.zero;
        attackBack = false;

        transform.localScale = new Vector3(Mathf.Abs(monsterX), monsterY, monsterZ);
    }

}