using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBossAttack : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > flyState.attackTime && !attackTrue)
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
                for(int i=0; i<5; i++)
                    Poison();
            }
            else if(randNum == 2)
            {
                Rush();
            }
            else if(randNum == 3)
            {
                Laser();
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

    IEnumerator ShakePoison()
    {
        // ������ �ð� ����
        float shakeTime = 1.0f;
        // ������ ���� ����
        float shakeIntensity = 0.08f;

        // ���� �ð� ���� ������ �������� ��ȭ��Ű�� ����
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
        // ������ ���� �� ������Ʈ ��ġ�� ���� ��ġ�� �ǵ���
        transform.position = originalPosition;
        flyState.speed = 5f;
        attackTrue = false;
    }

    IEnumerator ShakeRush()
    {
        // ������ �ð� ����
        float shakeTime = 1.0f;
        // ������ ���� ����
        float shakeIntensity = 0.08f;

        // ���� �ð� ���� ������ �������� ��ȭ��Ű�� ����
        Vector3 originalPosition = transform.position;
        while (shakeTime > 0)
        {
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.position = shakePosition;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        
        // ������ ���� �� ������Ʈ ��ġ�� ���� ��ġ�� �ǵ���
        transform.position = originalPosition;
        rushTrue = true;
        StartCoroutine(RushTime());
    }

    IEnumerator RushTime()
    {
        yield return new WaitForSeconds(1.5f);
        rushTrue = false;
        rushTime = false;
        flyState.speed = 5f;
        flyMove.rushSpeed = 15f;
        attackTrue = false;
    }

    IEnumerator ShakeLaser()
    {
        // ������ �ð� ����
        float shakeTime = 1.0f;
        // ������ ���� ����
        float shakeIntensity = 0.08f;

        // ���� �ð� ���� ������ �������� ��ȭ��Ű�� ����
        Vector3 originalPosition = transform.position;
        while (shakeTime > 0)
        {
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.position = shakePosition;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        // ������ ���� �� ������Ʈ ��ġ�� ���� ��ġ�� �ǵ���
        transform.position = originalPosition;
        laser.SetActive(false);
        ani.SetBool("Laser", false);
        flyState.speed = 5f;
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
