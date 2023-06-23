using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardState : MonoBehaviour
{
    [SerializeField] private WizardAttack wizardAttack;

    GameObject[] wizardBody = new GameObject[5];

    public float hpMax = 10f;
    public float hp = 10f;
    public float speed = 4f;
    public float damage = 2f;
    public float attackTime = 5f;

    public bool berserker;

    public Image hpBar;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            wizardBody[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < hpMax / 2 && !wizardAttack.attackTrue && !berserker)
            Berserker();
        HpVar();
    }

    void Berserker()
    {
        berserker = true;
        wizardAttack.attackTrue = true;
        attackTime = 3f;
        StartCoroutine(ShakeBerserk());
    }



    void HpVar()
    {
        hpBar.fillAmount = hp / hpMax;
    }

    IEnumerator ShakeBerserk()
    {
        // ������ �ð� ����
        float shakeTime = 1.0f;
        // ������ ���� ����
        float shakeIntensity = 0.06f;

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
        Color newColor = new Color32(180, 43, 43, 255); // 180 43 43
        for (int i = 0; i < 4; i++)
        {
            wizardBody[i].GetComponent<Renderer>().material.color = newColor;
        }
        wizardAttack.attackTrue = false;
    }

}
