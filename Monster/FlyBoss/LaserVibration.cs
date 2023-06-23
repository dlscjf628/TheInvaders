using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVibration : MonoBehaviour
{
    float damage = 10f;
    [SerializeField] private GameObject FlyBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FlyBoss.GetComponent<FlyBossState>().hp <= 0)
            Destroy(gameObject);
        transform.position = new Vector2(FlyBoss.transform.position.x, FlyBoss.transform.position.y);
        float shakeTime = 1.0f;
        // 진동의 강도 설정
        float shakeIntensity = 0.1f;

        // 진동 시간 동안 진폭을 랜덤으로 변화시키며 진동
        Vector3 originalPosition = transform.position;
        while (shakeTime > 0)
        {
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.position = shakePosition;
            shakeTime -= Time.deltaTime;
        }
        // 진동이 끝난 후 오브젝트 위치를 원래 위치로 되돌림
        transform.position = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Damage target = collision.GetComponent<Damage>();
            target.OnDamage(damage, 1);
        }
    }
}
