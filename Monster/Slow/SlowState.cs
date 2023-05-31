using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowState : MonoBehaviour
{
    public float hp;  //몬스터 체력
    public float maxHp;
    public float speed; //몬스터 이동속도
    public float damage; //몬스터 데미지


    void OnEnable()
    {
        hp = maxHp;
    }

    public void Init(SpawnDataSlow data)
    {
        //ani.runtimeAnimatorController =  animCon[data.spriteType];
        speed = data.speed;
        maxHp = data.health;
        hp = data.health;
        damage = data.damage;
    }


}