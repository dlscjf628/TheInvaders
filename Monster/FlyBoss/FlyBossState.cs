using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyBossState : MonoBehaviour
{
    [SerializeField] private FlyBossAttack flyBossAttack;
    [SerializeField] private Image hpBar;

    public float hpMax = 10f;
    public float hp = 1f;
    public float speed = 5f;
    public float damage = 2f;
    public float attackTime = 5f;

    GameObject[] flyBossBody = new GameObject[4];

    bool berserker;
    
    private void Start()
    {
        for(int i=0; i<4; i++)
        {
            flyBossBody[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (hp < hpMax / 3 && !flyBossAttack.attackTrue && !berserker)
            Berserker();
        HpVar();
    }

    void HpVar()
    {
        hpBar.fillAmount = hp / hpMax;
    }

    void Berserker()
    {
        berserker = true;
        flyBossAttack.attackTrue = true;
        attackTime = 3f;
        StartCoroutine(ShakeBerserk());
    }

    IEnumerator ShakeBerserk()
    {
        // 진동할 시간 설정
        float shakeTime = 1.0f;
        // 진동의 강도 설정
        float shakeIntensity = 0.09f;

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
        for(int i=0; i<4; i++)
        {
            flyBossBody[i].GetComponent<Renderer>().material.color = newColor;
        }
        flyBossAttack.attackTrue = false;
    }

}
