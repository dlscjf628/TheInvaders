using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantState : MonoBehaviour
{
    public float hp;  //���� ü��
    public float maxHp;
    public float speed; //���� �̵��ӵ�
    public float damage; //���� ������


    void OnEnable()
    {
        hp = maxHp;
    }

    public void Init(SpawnDataMutant data)
    {
        //ani.runtimeAnimatorController =  animCon[data.spriteType];
        speed = data.speed;
        maxHp = data.health;
        hp = data.health;
        damage = data.damage;
    }


}