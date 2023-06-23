using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantRat : MonoBehaviour, Damage
{
    MutantState mState;

    GameObject player;

    Rigidbody2D rigid;
    Collider2D coll;

    WaitForFixedUpdate wait;

    Spawner spawn;

    bool attackBack;
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

        wait = new WaitForFixedUpdate();

        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
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
        GameObject effect = Manager.instance.pool.Geteffect();
        effect.transform.position = new Vector2(transform.position.x, transform.position.y);

        mState.hp -= damgage;

        if (!attackBack)
            StartCoroutine(Back());

        if (mState.hp <= 0)
        {
            isLive = false;
            rigid.simulated = false;
            coll.enabled = false;
            int n = Random.Range(0, 5);
            if (n < 3)
            {
                GameObject coin = Manager.instance.pool.GetCoin();
                coin.transform.position = new Vector2(transform.position.x, transform.position.y);
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
        mState = GetComponent<MutantState>();
        player = GameObject.FindGameObjectWithTag("Player");

        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spawn = GameObject.Find("PoolManager").gameObject.transform.GetChild(0).gameObject.GetComponent<Spawner>();
        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;
        rigid.velocity = Vector2.zero;
        attackBack = false;
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
        transform.localScale = new Vector3(Mathf.Abs(monsterX), monsterY, monsterZ);
    }

}
