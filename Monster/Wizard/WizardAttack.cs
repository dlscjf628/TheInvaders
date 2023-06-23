using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizardAttack : MonoBehaviour, Damage
{
    [SerializeField] private WizardState wizardState;
    public GameObject skullPre;
    public GameObject PoisonskullPre;
    public GameObject shadowStonePre;
    public GameObject stonePre;

    Animator staffAni;

    float t, t1;
    public bool attackTrue;

    float shakeTime = 1.0f;
    float shakeIntensity = 0.01f;

    int skullCount = 10;
    int stoneCount = 50;
    int randNum = 0;

    SpriteRenderer[] render = new SpriteRenderer[5];
    bool DieMotion;
    Collider2D coll;
    int cnt;
    float color1;
    public bool isLive = true;

    InforMation info;
    GameObject pool;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            render[i] = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        }
        staffAni = transform.GetChild(4).gameObject.GetComponent<Animator>();
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        pool = GameObject.Find("PoolManager");
        coll = GetComponent<Collider2D>();
        color1 = 255f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.gameOver)
        {
            Shake();
            t += Time.deltaTime;
            if (t > wizardState.attackTime && !attackTrue && !DieMotion && isLive)
            {
                t = 0;
                //int randNum;
                //randNum = Random.Range(1, 11);

                if (randNum == 0)
                    randNum = 1;
                else if (randNum == 1)
                    randNum = 5;
                else if (randNum == 5)
                    randNum = 8;
                else if (randNum == 8)
                    randNum = 1;

                if (randNum >= 1 && randNum <= 4)
                {
                    SkullAttack();
                }
                else if (randNum >= 5 && randNum <= 7)
                {
                    PoisonSkullAttack();
                }
                else if (randNum >= 8 && randNum <= 10)
                {
                    StoneAttack();
                }
                else if (randNum >= 11 && randNum <= 12)
                {

                }
            }
            if (DieMotion)
            {
                attackTrue = true;
                isLive = false;
                coll.enabled = false;
                color1 *= 0.999f;
                for (int i = 0; i < 5; i++)
                {
                    Color color = new Color(1f, 1f, 1f, color1 / 255f);

                    render[i].color = color;
                }
                if (color1 < 30f)
                {
                    //StartCoroutine(NextStage());
                    Destroy(transform.parent.gameObject);
                    
                }
            }
        }
    }

    public virtual void OnDamage(float damgage, int mode)
    {
        GameObject effect = Manager.instance.pool.Geteffect();
        effect.transform.position = new Vector2(transform.position.x, transform.position.y);

        wizardState.hp -= damgage;
        if (wizardState.hp <= 0 && cnt == 0)
        {
            cnt++;
            isLive = false;
            StartCoroutine(ShakeDie());
            coll.enabled = false;
            DieMotion = true;
            info.Coin += 200;
        }
    }

    void SkullAttack()
    {
        attackTrue = true;
        wizardState.speed = 0f;
        staffAni.SetBool("Staff2", true);
        StartCoroutine(CoSkullAttack());
    }

    void PoisonSkullAttack()
    {
        attackTrue = true;
        wizardState.speed = 0f;
        staffAni.SetBool("Staff2", true);
        StartCoroutine(CoPoisonSkullAttack());
    }

    void StoneAttack()
    {
        attackTrue = true;
        wizardState.speed = 0f;
        staffAni.SetBool("Staff1", true);
        StartCoroutine(CoStoneAttack());
    }


    void Shake()
    {
        Vector3 originalPosition = transform.position;
        Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
        transform.position = shakePosition;
        shakeTime -= Time.deltaTime;
    }


    //IEnumerator NextStage()
    //{
    //    yield return new WaitForSeconds(3f);
    //    info.Stage++;
    //    Manager.instance.level++;
    //    SceneManager.LoadScene("ShopGUI");
    //}

    IEnumerator ShakeDie()
    {
        // 진동할 시간 설정
        float shakeTime = 3.0f;
        // 진동의 강도 설정
        float shakeIntensity = 0.8f;

        // 진동 시간 동안 진폭을 랜덤으로 변화시키며 진동
        Vector3 originalPosition = transform.position;
        while (shakeTime > 0)
        {
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.position = shakePosition;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        // 진동이 끝난 후 오브젝트 위치를 원래 위치로 되돌림
        transform.position = originalPosition;
        Color newColor = new Color32(180, 43, 43, 255); // 180 43 43
        attackTrue = true;
    }

    IEnumerator CoSkullAttack()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < skullCount; i++)
        {
            GameObject fireball = Instantiate(skullPre, transform.position, Quaternion.identity);
        }

        attackTrue = false;
        staffAni.SetBool("Staff2", false);
        wizardState.speed = 4f;
    }
    IEnumerator CoPoisonSkullAttack()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < skullCount; i++)
        {
            GameObject fireball = Instantiate(PoisonskullPre, transform.position, Quaternion.identity);
        }
        attackTrue = false;
        staffAni.SetBool("Staff2", false);
        wizardState.speed = 4f;
    }

    IEnumerator CoStoneAttack()
    {
        for (int i = 0; i < stoneCount; i++)
        {
            Vector2 randPosition = new Vector2(Random.Range(-24f, 24f), Random.Range(-13f, 13f));
            Instantiate(shadowStonePre, randPosition, Quaternion.identity);
            Instantiate(stonePre, new Vector2(randPosition.x, randPosition.y + 9), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
        attackTrue = false;
        wizardState.speed = 4f;
        staffAni.SetBool("Staff1", false);
        t = 0f;
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
                target.OnDamage(wizardState.damage, 1);
            }
        }
    }

}
