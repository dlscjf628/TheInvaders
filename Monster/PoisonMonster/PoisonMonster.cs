using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMonster : MonoBehaviour, Damage
{
    [SerializeField] PoisonState mState;

    GameObject player;

    Rigidbody2D rigid;
    Collider2D coll;

    Spawner spawn;

    bool isLive;
    bool attackBack;

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
        if (!Manager.instance.gameOver && !attackBack && isLive)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * mState.speed * Time.deltaTime);
            rigid.velocity = Vector2.zero;
        }
        else if (attackBack)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 0.1f, ForceMode2D.Impulse);
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

    IEnumerator Back()
    {
        attackBack = true;
        yield return new WaitForSeconds(0.1f);
        attackBack = false;

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
        spawn = GameObject.Find("PoolManager").gameObject.transform.GetChild(0).gameObject.GetComponent<Spawner>();
        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;
        rigid.velocity = Vector2.zero;
        attackBack = false;

    }

}
