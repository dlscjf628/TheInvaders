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
        flyState.speed = 5f;
    }

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
        flyState.speed = 5f;
    }
}
