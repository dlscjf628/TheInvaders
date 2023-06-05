using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InforMation : MonoBehaviour
{
    public int PlayerJob; // 플레이어 직업 0,1,2,3(검사, 총사, 마법사, 도박사)
    public int Stage = 1; // 스테이지 정보
    public float PlayerHP = 20; // 플레이어 HP
    public float PlayerMaxHP = 20;// 플레이어 MaxHP
    public int Coin = 0; //코인 보유 수
    public int ResetCoin = 1; // 리셋할때 필요한 코인
    public float PlayerSpeed = 1f;//플레이어 이동속도

    public Sprite[] PlayerWeapon = new Sprite[6]; // 플레이어가 지금 가지고 있는 무기

    public int[] weaponname = new int[6];//장착중인 무기 이름 0~11 
    public int[] WeaponGrade = new int[6]; //장착중인 무기 등급
    public int[] WearWeapondamage = new int[6]; // 현재 장착중인 무기 데미지
    public float[] WearWeaponspeed = new float[6]; // 현재 장착중인 무기 공격속도
    public int[] WearWeaponPrice = new int[6]; // 현재 장착중인 무기 가격

    public int[] Itemprice = new int[4]; // 아이템 가격

    //검 데미지
    public int[] Daggerdamage = new int[4]; //단검 데미지
    public int[] Sworddamge = new int[4]; //검 데미지
    public int[] Speardamge = new int[4]; //창 데미지
    public int[] Bluntdamge = new int[4]; //둔기 데미지
    //검 공속
    public float[] Daggerspeed = new float[4]; //단검 공속
    public float[] Swordspeed = new float[4]; //검 공속
    public float[] Spearspeed = new float[4]; //창 공속
    public float[] Bluntspeed = new float[4]; //둔기 공속
    //검 가격
    public int[] Daggerprice = new int[4];
    public int[] Swordprice = new int[4];
    public int[] Spearprice = new int[4];
    public int[] Bluntprice = new int[4];

    //총 데미지
    public int[] Pistoldamge = new int[4]; //권총 데미지
    public int[] AKdamge = new int[4]; //돌격소총 데미지
    public int[] Shotgundamge = new int[4]; //샷건 데미지
    public int[] Sinperdamge = new int[4]; //저격소총 데미지
    //총 공속
    public float[] Pistolspeed = new float[4]; //권총 공속
    public float[] AKspeed = new float[4]; //돌격소총 공속
    public float[] Shotgunspeed = new float[4]; //샷건 공속
    public float[] Sinperspeed = new float[4]; //저격소총 공속
    //총 가격
    public int[] Pistolprice = new int[4];
    public int[] AKprice = new int[4];
    public int[] Shotgunprice = new int[4];
    public int[] Sinperprice = new int[4];

    //마법 데미지
    public int[] Firedamge = new int[4]; //불 데미지
    public int[] Thunderdamge = new int[4]; //전기 데미지
    public int[] Voiddamge = new int[4]; //암속 데미지
    public int[] Holydamge = new int[4]; //성속 데미지
    //마법 공속
    public float[] Firespeed = new float[4]; //불 공속
    public float[] Thunderspeed = new float[4]; //전기 공속
    public float[] Voidspeed = new float[4]; //암속 공속
    public float[] Holyspeed = new float[4]; //성속 공속
    //마법 가격
    public int[] Fireprice = new int[4];
    public int[] Thunderprice = new int[4];
    public int[] Voidprice = new int[4];
    public int[] Holyprice = new int[4];
}
