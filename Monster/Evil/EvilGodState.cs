using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilGodState : MonoBehaviour
{
    [SerializeField] private EvilGodAttack evilgodAttack;

    public float hpMax = 10f;
    public float hp = 10f;
    public float speed = 5f;
    public float damage = 2f;
    public float attackTime = 3f;

    public bool berserker;

    public Image hpBar;

    GameObject[] EvilBossBody = new GameObject[4];
    public GameObject wood1, wood2, wood3, wood4;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            EvilBossBody[i] = transform.GetChild(i).gameObject;
        }
        
    }

    private void Update()
    {
        if (hp < hpMax * 0.3f && !berserker && !evilgodAttack.attackTrue)
            Berserker();
        if(hp < hpMax * 0.6f)
        {
            Wood();
        }
        HpVar();
    }

    void HpVar()
    {
        hpBar.fillAmount = hp / hpMax;
    }

    void Wood()
    {
        wood1.SetActive(true);
        wood2.SetActive(true);
        wood3.SetActive(true);
        wood4.SetActive(true);
    }

    void Berserker()
    {
        berserker = true;
        evilgodAttack.attackTrue = true;
        attackTime = 2f;
        StartCoroutine(ShakePoison());
    }

    IEnumerator ShakePoison()
    {
        // ������ �ð� ����
        float shakeTime = 1.0f;
        // ������ ���� ����
        float shakeIntensity = 0.8f;

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
            EvilBossBody[i].GetComponent<Renderer>().material.color = newColor;
        }
        evilgodAttack.attackTrue = false;
    }

}