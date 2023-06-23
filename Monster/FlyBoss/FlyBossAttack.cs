using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyBossAttack : MonoBehaviour, Damage
{
    [SerializeField] private FlyBossState flyState;
    [SerializeField] private FlyBossMove flyMove;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject PoisonBall;
    GameObject player;
    Animator ani;

    float t, t1;
    int randNum;
    public bool rushTrue;
    public bool rushTime;
    public bool attackTrue;

    Vector2 v;
    public Vector2 rushPosition;

    SpriteRenderer[] render = new SpriteRenderer[4];
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
        for (int i = 0; i < 4; i++)
        {
            render[i] = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        }
        player = GameObject.Find("Player");
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        pool = GameObject.Find("PoolManager");
        ani = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        color1 = 255f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.gameOver)
        {
            t += Time.deltaTime;
            if (t > flyState.attackTime && !attackTrue && !DieMotion && isLive)
            {
                t = 0;
                //randNum = Random.Range(1, 4);
                if (randNum == 0)
                    randNum = 1;
                else if (randNum == 1)
                    randNum = 2;
                else if (randNum == 2)
                    randNum = 3;
                else if (randNum == 3)
                    randNum = 1;
                if (randNum == 1)
                {
                    for (int i = 0; i < 10; i++)
                        Poison();
                }
                else if (randNum == 2)
                {
                    Rush();
                }
                else if (randNum == 3)
                {
                    Laser();
                }
            }

            if (DieMotion)
            {
                attackTrue = true;
                isLive = false;
                coll.enabled = false;
                color1 *= 0.999f;
                for (int i = 0; i < 4; i++)
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

    void Poison()
    {
        attackTrue = true;
        flyState.speed = 0f;
        StartCoroutine(ShakePoison());
    }

    void Rush()
    {
        attackTrue = true;
        flyState.speed = 0f;
        StartCoroutine(ShakeRush());
    }

    void Laser()
    {
        attackTrue = true;
        flyState.speed = 0f;
        ani.SetBool("Laser", true);
        StartCoroutine(ShakeLaser());
    }
    
    void LaserOn()
    {
        laser.SetActive(true);
    }

    public virtual void OnDamage(float damgage, int mode)
    {
        GameObject effect = Manager.instance.pool.Geteffect();
        effect.transform.position = new Vector2(transform.position.x, transform.position.y);


        flyState.hp -= damgage;
        if (flyState.hp <= 0 && cnt == 0)
        {
            cnt++;
            StartCoroutine(ShakeDie());
            coll.enabled = false;
            DieMotion = true;
            info.Coin += 100;
        }
    }

    IEnumerator ShakePoison()
    {
        // 진동할 시간 설정
        float shakeTime = 1.0f;
        // 진동의 강도 설정
        float shakeIntensity = 0.08f;

        // 진동 시간 동안 진폭을 랜덤으로 변화시키며 진동
        Vector3 originalPosition = transform.position;
        while (shakeTime > 0)
        {
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.position = shakePosition;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        v = new Vector2(transform.position.x, transform.position.y - 0.2f);
        Instantiate(PoisonBall, v, Quaternion.identity);
        // 진동이 끝난 후 오브젝트 위치를 원래 위치로 되돌림
        transform.position = originalPosition;
        flyState.speed = 3f;
        attackTrue = false;
    }

    //IEnumerator NextStage()
    //{
    //    yield return new WaitForSeconds(3f);
    //    info.Stage++;
    //    Manager.instance.level++;
    //    player.transform.position = new Vector2(0f, 0f);
    //    SceneManager.LoadScene("ShopGUI");
    //}

    IEnumerator ShakeRush()
    {
        // 진동할 시간 설정
        float shakeTime = 1.0f;
        // 진동의 강도 설정
        float shakeIntensity = 0.08f;

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
        rushTrue = true;
        StartCoroutine(RushTime());
    }

    IEnumerator RushTime()
    {
        yield return new WaitForSeconds(1.5f);
        rushTrue = false;
        rushTime = false;
        flyState.speed = 3f;
        flyMove.rushSpeed = 15f;
        attackTrue = false;
    }

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

    IEnumerator ShakeLaser()
    {
        // 진동할 시간 설정
        float shakeTime = 1.0f;
        // 진동의 강도 설정
        float shakeIntensity = 0.08f;

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
        laser.SetActive(false);
        ani.SetBool("Laser", false);
        flyState.speed = 3f;
        attackTrue = false;
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
                target.OnDamage(flyState.damage, 1);
            }
        }
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Wall")
    //    {
    //        flyMove.rushSpeed = 0f;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Wall")
    //    {
    //        flyMove.rushSpeed = 15f;
    //    }
    //}
}
