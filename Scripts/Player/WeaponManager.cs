using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //현재 오브젝트가 몇번째 오브젝트인지 확인.
    //information의 배열 순서와 동일
    public int ObjectNumber;

    Animator ani;
    SpriteRenderer SR;

    //근접 애니메이터
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Knife;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Sword;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Spear;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Mace;
    //총  애니메이터
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_HandGun;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_ShotGun;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Sniper;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Ak47;
    //마법 애니메이터
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Meteo;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_BlackHole;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Holy;
    [SerializeField] private RuntimeAnimatorController WeaponAnimator_Lightning;

    //근접 
    [SerializeField] private Sprite Knife;
    [SerializeField] private Sprite Sword;
    [SerializeField] private Sprite Spear;
    [SerializeField] private Sprite Mace;
    //총 
    [SerializeField] private Sprite HandGun;
    [SerializeField] private Sprite ShotGun;
    [SerializeField] private Sprite Sniper;
    [SerializeField] private Sprite Ak47;
    //마법
    [SerializeField] private Sprite Meteo;
    [SerializeField] private Sprite BlackHole;
    [SerializeField] private Sprite Holy;
    [SerializeField] private Sprite Lightning;
    CircleCollider2D parentCollider;

    public float inforealanispeed;
    public float inforealdamage;
    public int infoweaponname;
    float newRadius;

    public bool leftobj;

    public InforMation info;

    bool Damage = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        Damage = false;
        ani = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        parentCollider = transform.parent.GetComponent<CircleCollider2D>();
        info = GameObject.Find("GameManager").GetComponent<InforMation>();

        //****************************************************
        //근접.
        if (info.weaponname[ObjectNumber] == 0)
        {
            SR.sprite = Knife;
            ani.runtimeAnimatorController = WeaponAnimator_Knife;
            newRadius = 1.5f;

        }
        else if (info.weaponname[ObjectNumber] == 1)
        {
            SR.sprite = Sword;
            ani.runtimeAnimatorController = WeaponAnimator_Sword;
            newRadius = 1.7f;
        }
        else if (info.weaponname[ObjectNumber] == 2)
        {
            SR.sprite = Spear;
            ani.runtimeAnimatorController = WeaponAnimator_Spear;
            newRadius = 1.5f;
        }
        else if (info.weaponname[ObjectNumber] == 3)
        {
            SR.sprite = Mace;
            ani.runtimeAnimatorController = WeaponAnimator_Mace;
            newRadius = 1.7f;
        }
        //****************************************************
        //총
        else if (info.weaponname[ObjectNumber] == 4)
        {
            SR.sprite = HandGun;
            ani.runtimeAnimatorController = WeaponAnimator_HandGun;
            newRadius = 3f;
        }
        else if (info.weaponname[ObjectNumber] == 5)
        {
            SR.sprite = Ak47;
            ani.runtimeAnimatorController = WeaponAnimator_Ak47;
            newRadius = 3f;
        }
        else if (info.weaponname[ObjectNumber] == 6)
        {
            SR.sprite = ShotGun;
            ani.runtimeAnimatorController = WeaponAnimator_ShotGun;
            newRadius = 3f;
        }
        else if (info.weaponname[ObjectNumber] == 7)
        {
            SR.sprite = Sniper;
            ani.runtimeAnimatorController = WeaponAnimator_Sniper;
            newRadius = 4f;
        }
        //****************************************************
        //마법
        else if (info.weaponname[ObjectNumber] == 8)
        {
            SR.sprite = Meteo;
            ani.runtimeAnimatorController = WeaponAnimator_Meteo;
            newRadius = 2.5f;
        }
        else if (info.weaponname[ObjectNumber] == 9)
        {
            SR.sprite = Lightning;
            ani.runtimeAnimatorController = WeaponAnimator_Lightning;
            newRadius = 2.5f;
        }
        else if (info.weaponname[ObjectNumber] == 10)
        {
            SR.sprite = BlackHole;
            ani.runtimeAnimatorController = WeaponAnimator_BlackHole;
            newRadius = 2.5f;
        }
        else if (info.weaponname[ObjectNumber] == 11)
        {
            SR.sprite = Holy;
            ani.runtimeAnimatorController = WeaponAnimator_Holy;
            newRadius = 2.5f;
        }
        parentCollider.radius = newRadius;

    }
    private void Update()
    {
        if (Damage == false)
        {
            inforealdamage = info.WearWeapondamage[ObjectNumber];
            inforealanispeed = info.WearWeaponspeed[ObjectNumber];

            gameObject.GetComponent<StopAni>().damage = inforealdamage;
            gameObject.GetComponent<StopAni>().anispeed = inforealanispeed;
            gameObject.GetComponent<StopAni>().realdamage = inforealdamage;
            gameObject.GetComponent<StopAni>().realanispeed = inforealanispeed;
            Damage = true;

        }

    }
}
