using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBossAttack : MonoBehaviour
{
    [SerializeField] private FlyBossState flyState;
    [SerializeField] GameObject PoisonBall;
    GameObject player;

    float t;
    int randNum;
    public bool rushTrue;
    public bool rushTime;

    Vector2 v;
    public Vector2 rushPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 5)
        {
            t = 0;
            //randNum = Random.Range(1, 4);
            randNum = 2;
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

            }
        }
    }

    void Poison()
    {
        flyState.speed = 0f;
        StartCoroutine(ShakePoison());
    }

    void Rush()
    {
        flyState.speed = 0f;
        StartCoroutine(ShakeRush());
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
    }
}
