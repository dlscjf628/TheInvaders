using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilGodAttack : MonoBehaviour, Damage
{
    [SerializeField] private EvilGodState evilState;
    public GameObject fireballPrefab;

    SpriteRenderer[] render = new SpriteRenderer[6];
    Collider2D coll;

    float fireballSpeed = 5f;
    float angleStep = 15f;
    int fireballCount = 24;


    public bool attackTrue;
    bool berserkerMode;

    bool DieMotion;

    int randNum = 0;
    int cnt;
    float t, t1;

    public bool isLive = true;
    float color1;

    InforMation info;
    GameObject pool;
    GameObject manager;

    Vector3 fPosition1, fPosition2;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<6; i++)
        {
            render[i] = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        }
        coll = GetComponent<Collider2D>();
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        pool = GameObject.Find("PoolManager");
        manager = GameObject.Find("GameManager");
        color1 = 255f;
        fPosition1 = new Vector3((float)(transform.position.x - 0.2), (float)(transform.position.y - 1f), transform.position.z);
        fPosition2 = new Vector3((float)(transform.position.x + 0.2), (float)(transform.position.y - 1f), transform.position.z);
    }

    void Update()
    {
        if (!Manager.instance.gameOver)
        {
            t += Time.deltaTime;
            if (t > evilState.attackTime && !attackTrue && !DieMotion && isLive)
            {
                t = 0;

                //randNum = Random.Range(1, 10);
                if (randNum == 0)
                    randNum = 1;
                else if (randNum == 1)
                    randNum = 6;
                else if (randNum == 6)
                    randNum = 8;
                else if (randNum == 8)
                    randNum = 1;

                if (randNum >= 1 && randNum <= 5)
                {
                    NormalFireBall();
                }
                else if (randNum == 6 || randNum == 7)
                {
                    SpFireBallOwn();
                }
                else if (randNum == 8 || randNum == 9)
                {
                    SpFireBallTwo();
                }
            }
            if (evilState.berserker && !berserkerMode)
            {
                berserkerMode = true;
                fireballSpeed = 7f;
                angleStep = 12f;
                fireballCount = 30;
            }
            if (DieMotion)
            {
                attackTrue = true;
                isLive = false;
                coll.enabled = false;
                color1 *= 0.999f;
                for (int i = 0; i < 6; i++)
                {
                    Color color = new Color(1f, 1f, 1f, color1 / 255f);

                    render[i].color = color;
                }
                if (color1 < 30f)
                {
                    Destroy(transform.parent.gameObject);
                    //StartCoroutine(NextStage());
                }
            }
        }
    }

    void NormalFireBall()
    {
        attackTrue = true;
        float currentAngle = -90f;
        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.right;
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = fireball.GetComponent<Rigidbody2D>();
            rb2d.velocity = direction * fireballSpeed;
            currentAngle += angleStep;
        }
        attackTrue = false;
    }
    void SpFireBallOwn()
    {
        attackTrue = true;
        for (int i = 0; i < 4; i++)
            StartCoroutine(CoSpFireBallOwn(i));
    }

    void SpFireBallTwo()
    {
        attackTrue = true;
        for(int i=0; i<4; i++)
            StartCoroutine(CoSpFireBallTwo(i));
    }

    public virtual void OnDamage(float damgage, int mode)
    {
        evilState.hp -= damgage;
        if (evilState.hp <= 0 && cnt == 0)
        {
            cnt++;
            StartCoroutine(ShakePoison());
            coll.enabled = false;
            DieMotion = true;
        }
    }

    //IEnumerator NextStage()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Destroy(pool);
    //    Destroy(manager);
    //    SceneManager.LoadScene("Eending");
    //}

    IEnumerator CoSpFireBallTwo(int a)
    {
        float[] currentAngle = new float[] { 0, 90, 180, 270 };

        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(currentAngle[a], Vector3.forward) * Vector3.right;
            GameObject fireball1 = Instantiate(fireballPrefab, fPosition1, Quaternion.identity);
            GameObject fireball2 = Instantiate(fireballPrefab, fPosition2, Quaternion.identity);

            Rigidbody2D rb2d1 = fireball1.GetComponent<Rigidbody2D>();
            rb2d1.velocity = direction * fireballSpeed;
            Rigidbody2D rb2d2 = fireball2.GetComponent<Rigidbody2D>();
            rb2d2.velocity = direction * fireballSpeed;
            currentAngle[a] += angleStep;
            yield return new WaitForSeconds(0.1f);
        }
        
        attackTrue = false;
    }

    IEnumerator CoSpFireBallOwn(int a)
    {
        float[] currentAngle = new float[] { 0, 90, 180, 270 };
        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(currentAngle[a], Vector3.forward) * Vector3.right;
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = fireball.GetComponent<Rigidbody2D>();
            rb2d.velocity = direction * fireballSpeed;
            currentAngle[a] += angleStep;
            yield return new WaitForSeconds(0.1f);
        }
        attackTrue = false;
    }

    IEnumerator ShakePoison()
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t1 += Time.deltaTime;
            if (t1 > 0.5f)
            {
                t1 = 0;
                Damage target = collision.GetComponent<Damage>();
                target.OnDamage(evilState.damage, 1);
            }
        }
    }

}
