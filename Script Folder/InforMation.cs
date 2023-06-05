using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InforMation : MonoBehaviour
{
    public int PlayerJob; // �÷��̾� ���� 0,1,2,3(�˻�, �ѻ�, ������, ���ڻ�)
    public int Stage = 1; // �������� ����
    public float PlayerHP = 20; // �÷��̾� HP
    public float PlayerMaxHP = 20;// �÷��̾� MaxHP
    public int Coin = 0; //���� ���� ��
    public int ResetCoin = 1; // �����Ҷ� �ʿ��� ����
    public float PlayerSpeed = 1f;//�÷��̾� �̵��ӵ�

    public Sprite[] PlayerWeapon = new Sprite[6]; // �÷��̾ ���� ������ �ִ� ����

    public int[] weaponname = new int[6];//�������� ���� �̸� 0~11 
    public int[] WeaponGrade = new int[6]; //�������� ���� ���
    public int[] WearWeapondamage = new int[6]; // ���� �������� ���� ������
    public float[] WearWeaponspeed = new float[6]; // ���� �������� ���� ���ݼӵ�
    public int[] WearWeaponPrice = new int[6]; // ���� �������� ���� ����

    public int[] Itemprice = new int[4]; // ������ ����

    //�� ������
    public int[] Daggerdamage = new int[4]; //�ܰ� ������
    public int[] Sworddamge = new int[4]; //�� ������
    public int[] Speardamge = new int[4]; //â ������
    public int[] Bluntdamge = new int[4]; //�б� ������
    //�� ����
    public float[] Daggerspeed = new float[4]; //�ܰ� ����
    public float[] Swordspeed = new float[4]; //�� ����
    public float[] Spearspeed = new float[4]; //â ����
    public float[] Bluntspeed = new float[4]; //�б� ����
    //�� ����
    public int[] Daggerprice = new int[4];
    public int[] Swordprice = new int[4];
    public int[] Spearprice = new int[4];
    public int[] Bluntprice = new int[4];

    //�� ������
    public int[] Pistoldamge = new int[4]; //���� ������
    public int[] AKdamge = new int[4]; //���ݼ��� ������
    public int[] Shotgundamge = new int[4]; //���� ������
    public int[] Sinperdamge = new int[4]; //���ݼ��� ������
    //�� ����
    public float[] Pistolspeed = new float[4]; //���� ����
    public float[] AKspeed = new float[4]; //���ݼ��� ����
    public float[] Shotgunspeed = new float[4]; //���� ����
    public float[] Sinperspeed = new float[4]; //���ݼ��� ����
    //�� ����
    public int[] Pistolprice = new int[4];
    public int[] AKprice = new int[4];
    public int[] Shotgunprice = new int[4];
    public int[] Sinperprice = new int[4];

    //���� ������
    public int[] Firedamge = new int[4]; //�� ������
    public int[] Thunderdamge = new int[4]; //���� ������
    public int[] Voiddamge = new int[4]; //�ϼ� ������
    public int[] Holydamge = new int[4]; //���� ������
    //���� ����
    public float[] Firespeed = new float[4]; //�� ����
    public float[] Thunderspeed = new float[4]; //���� ����
    public float[] Voidspeed = new float[4]; //�ϼ� ����
    public float[] Holyspeed = new float[4]; //���� ����
    //���� ����
    public int[] Fireprice = new int[4];
    public int[] Thunderprice = new int[4];
    public int[] Voidprice = new int[4];
    public int[] Holyprice = new int[4];
}
