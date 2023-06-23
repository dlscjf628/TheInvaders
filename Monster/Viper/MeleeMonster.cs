using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : MonoBehaviour, Damage
{
    ViperState mState;
    ViperHard viper;

    GameObject player;

    Vector3 rushPosition;

    Rigidbody2D rigid;
    Collider2D coll;

    Spawner spawn;

    bool isLive;
    bool attackBack;
    public bool hard;
    public bool rushTime;

    float t = 7f;

    float monsterX, monsterY, monsterZ;
    // Start is called before the first frame update
    void Awake()
    {
        mState = GetComponent<ViperState>();
        viper = GetComponent<ViperHard>();
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
            Hard();
            rigid.velocity = Vector2.zero;
        }
        //else if (attackBack)
        //{
        //    Vector3 playerPos = player.transform.position;
        //    Vector3 dirVec = transform.position - playerPos;
        //    rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        //}
    }

    void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (!viper.rush)
        {
            transform.Translate(direction * mState.speed * Time.deltaTime);
        }
        else
        {
            if (!rushTime)
            {
                rushTime = true;
                rushPosition = (player.transform.position - transform.position).normalized;
            }
            transform.Translate(rushPosition * mState.speed * Time.deltaTime);
        }

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
        //if (!attackBack)
        //{
        //    StartCoroutine(Back());
        //}
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

    void Hard()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        t += Time.deltaTime;
        if (distance <= 5.0f && hard == false && t >= 7f)
        {
            t = 0;
            hard = true;
            //mState.speed = 0f;
        }
    }

    void OnEnable()
    {

        mState = GetComponent<ViperState>();
        viper = GetComponent<ViperHard>();
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
