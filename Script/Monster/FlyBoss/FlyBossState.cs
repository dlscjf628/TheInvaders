using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyBossState : MonoBehaviour
{
    public float hpMax = 10f;
    public float hp = 10f;
    public float speed = 5f;
    public float damage = 2f;
    public float attackTime = 5f;

    [SerializeField] private Image hpBar;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (hp < hpMax / 3)
            AttackTime();
    }

    void HpVar()
    {
        hpBar.fillAmount = hp / hpMax;
    }

    void AttackTime()
    {
        attackTime = 3f;
        StartCoroutine(ShakePoison());
    }

    IEnumerator ShakePoison()
    {
        // ������ �ð� ����
        float shakeTime = 1.0f;
        // ������ ���� ����
        float shakeIntensity = 1.0f;

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
        //Color newColor = new Color32(180, 43, 43, 255); // 180 43 43
        //GetComponent<Renderer>().material.color = newColor;
    }

}
