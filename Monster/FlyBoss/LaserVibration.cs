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
        // ������ ���� ����
        float shakeIntensity = 0.1f;

        // ���� �ð� ���� ������ �������� ��ȭ��Ű�� ����
        Vector3 originalPosition = transform.position;
        while (shakeTime > 0)
        {
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            transform.position = shakePosition;
            shakeTime -= Time.deltaTime;
        }
        // ������ ���� �� ������Ʈ ��ġ�� ���� ��ġ�� �ǵ���
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
