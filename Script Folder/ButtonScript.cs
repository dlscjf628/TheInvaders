using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public Sprite[] WearWeapon = new Sprite[6]; // 장착중인 무기
    public GameObject[] WearWeaponFrameObject; // 장착중인 무기 등급에 따라 장착중인 무기 프레임 변경
    
    public GameObject[] RandomItemImageObject; // 랜덤 무기 이미지
    public GameObject[] RandomItemBackgroundObject; // 랜덤 등급 배경

    public GameObject[] WearWeaponObject; // 장착중인 무기 이미지
    public GameObject[] WearItemObject; // 구매한 아이템 이미지

    public Sprite[] RandomWeaponSprite; //무기 이미지 12개 - 단검 검 둔기 창 / 권총 AK 샷건 스나 / 불 전기 성속 암속
    public Sprite[] RandomItemSprite; //아이템 이미지 6개 - HP증가, 데미지 증가, 공속 증가, 이속 증가, 코인+50, 코인+100

    public Sprite[] RandomWeaponBackgroundSprite; // 등급 배경 이미지
    public Sprite[] WearWeaponFrame;//장착 무기의 등급을 알려주는 프레임

    public GameObject WeaponPopUp; //장착중인 무기 팝업
    public GameObject PausePopUp; //일시정지 팝업

    public Text cointext; //현재 코인 갯수
    public Text Resetcointext; // 리셋시 필요한 코인 갯수
    public Text[] RandomItemname = new Text[4]; // 아이템 설명
    public Text[] BuyButtonprice = new Text[4]; // 구매버튼 코인 갯수

    GameObject GameManger;
    public GameObject pool;
    //랜덤 변수
    int Randombackground1, Randombackground2, Randombackground3, Randombackground4;
    int RandomItem1, RandomItem2, RandomItem3, RandomItem4;
    int Grade, num; //등급 변수, 장착중인 무기 중 몇번째인지 확인하기 위한 변수
    int[] RandomWeaponGrade = new int[4]; // 랜덤 무기 등급
    string[] WeaponOrItem = new string[4]; // 아이템인지 무기인지 확인
    int[] RandomItemprice = new int[4]; // 아이템 가격
    int[] Weaponindex = new int[4]; // 어떤 무기인지 알려주는 변수
    int[] Itemnum = new int[4]; // 어떤 아이템인지 확인
    Color color;
    void Start()
    {
        GameManger = GameObject.Find("GameManager");
        pool = GameObject.Find("PoolManager");
        pool.SetActive(false);

        //라운드별 등급 확률
        WeaponBackGroundRandom();
        //아이템 확률
        ItemRandom();
        //구매 버튼 활성화
        Buttonactivate();

        //코인 관련 텍스트 설정
        cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
        Resetcointext.text = "Reset : " + GameManger.GetComponent<InforMation>().ResetCoin.ToString();
        //장착중인 무기 찾기
        for(int i = 0; i < 6; i++)
        {
            WearWeapon[i] = GameManger.GetComponent<InforMation>().PlayerWeapon[i];
            //무기 이미지 변경
            if (WearWeapon[i] != null)
            {
                WearWeaponObject[i].GetComponent<Image>().sprite = WearWeapon[i];
                color = WearWeaponObject[i].GetComponent<Image>().color;
                color.a = 1f;
                WearWeaponObject[i].GetComponent<Image>().color = color;
            }
            if(GameManger.GetComponent<InforMation>().WeaponGrade[i] < 4)
            {
                WearWeaponFrameObject[i].GetComponent<Image>().sprite = WearWeaponFrame[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
            }
        }
    }

    //무기+아이템 이미지 설정
    public void ItemRandom()
    {
        //아이템 랜덤 변수 / 0 ~ 11까지는 무기 그 이후론 아이템
        RandomItem1 = Random.Range(0, 18);
        RandomItem2 = Random.Range(0, 18);
        RandomItem3 = Random.Range(0, 18);
        RandomItem4 = Random.Range(0, 18);

        //아이템 이미지 설정
        RandomRangeItem(0, RandomItem1);
        RandomRangeItem(1, RandomItem2);
        RandomRangeItem(2, RandomItem3);
        RandomRangeItem(3, RandomItem4);
    }
    // 변수에 따른 무기or아이템 설정
    public void RandomRangeItem(int index, int per)
    {
        if (per >= 0 && per < 12) // 무기
        {
            RandomItemImageObject[index].GetComponent<Image>().sprite = RandomWeaponSprite[per];
            WeaponOrItem[index] = "Weapon";
            Weaponindex[index] = per;
            WeaponStatetext(index, per);
        }
        else if (per > 11) // 아이템
        {
            per -= 12;
            RandomItemImageObject[index].GetComponent<Image>().sprite = RandomItemSprite[per];
            WeaponOrItem[index] = "Item";
            ItemStatetext(index, per);
        }
    }
    //아이템 배경 설정
    public void WeaponBackGroundRandom()
    {
        //등급 확률
        Randombackground1 = Random.Range(1, 101);
        Randombackground2 = Random.Range(1, 101);
        Randombackground3 = Random.Range(1, 101);
        Randombackground4 = Random.Range(1, 101);
        if (GameManger.GetComponent<InforMation>().Stage <= 5)
        {
            RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
            RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
            RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
            RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
            RandomWeaponGrade[0] = 0;
            RandomWeaponGrade[1] = 0;
            RandomWeaponGrade[2] = 0;
            RandomWeaponGrade[3] = 0;
        }
        else if (GameManger.GetComponent<InforMation>().Stage > 5 && GameManger.GetComponent<InforMation>().Stage <= 10) // 라운드 6~10 까지는 희귀(회색) 90%, 영웅(보라) 10%
        {
            //첫번째 카드 등급
            if (Randombackground1 <= 90)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[0] = 0;

            }
            else if (Randombackground1 > 90)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[0] = 1;
            }
            //2번째 카드 등급
            if (Randombackground2 <= 90)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[1] = 0;
            }
            else if (Randombackground2 > 90)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[1] = 1;
            }
            //3번째 카드 등급
            if (Randombackground3 <= 90)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[2] = 0;
            }
            else if (Randombackground3 > 90)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[2] = 1;
            }
            //4번째 카드 등급
            if (Randombackground4 <= 90)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[3] = 0;
            }
            else if (Randombackground4 > 90)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[3] = 1;
            }
        }
        else if (GameManger.GetComponent<InforMation>().Stage > 10 && GameManger.GetComponent<InforMation>().Stage <= 15) // 라운드 11~15 까지는 희귀(회색) 80%, 영웅(보라) 15%, 전설(노랑) 5%
        {
            //첫번째 카드 등급
            if (Randombackground1 <= 80)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[0] = 0;
            }
            else if (Randombackground1 > 80 && Randombackground1 <= 95)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[0] = 1;
            }
            else if (Randombackground1 > 95)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[0] = 2;
            }
            // 2번째 카드 등급
            if (Randombackground2 <= 80)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[1] = 0;
            }
            else if (Randombackground2 > 80 && Randombackground2 <= 95)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[1] = 1;
            }
            else if (Randombackground2 > 95)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[1] = 2;
            }
            //3번째 카드 등급
            if (Randombackground3 <= 80)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[2] = 0;
            }
            else if (Randombackground3 > 80 && Randombackground3 <= 95)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[2] = 1;
            }
            else if (Randombackground3 > 95)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[2] = 2;
            }
            //4번째 카드 등급
            if (Randombackground4 <= 80)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[3] = 0;
            }
            else if (Randombackground4 > 80 && Randombackground4 <= 95)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[3] = 1;
            }
            else if (Randombackground4 > 95)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[3] = 2;
            }
        }
        else if (GameManger.GetComponent<InforMation>().Stage > 15 && GameManger.GetComponent<InforMation>().Stage <= 20) // 라운드 16 ~ 20 일때 희귀(회색) 40%, 영웅(보라) 45%, 전설(노랑) 14%, 고대(파랑) 1%
        {
            // 첫번째 카드 등급
            if (Randombackground1 <= 40)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[0] = 0;
            }
            else if (Randombackground1 > 40 && Randombackground1 <= 85)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[0] = 1;
            }
            else if (Randombackground1 > 85 && Randombackground1 <= 99)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[0] = 2;
            }
            else if (Randombackground1 == 100)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[0] = 3;
            }
            // 2번째 카드 등급
            if (Randombackground2 <= 40)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[1] = 0;
            }
            else if (Randombackground2 > 40 && Randombackground2 <= 85)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[1] = 1;
            }
            else if (Randombackground2 > 85 && Randombackground2 <= 99)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[1] = 2;
            }
            else if (Randombackground2 == 100)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[1] = 3;
            }
            // 3번째 카드 등급
            if (Randombackground3 <= 40)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[2] = 0;
            }
            else if (Randombackground3 > 40 && Randombackground3 <= 85)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[2] = 1;
            }
            else if (Randombackground3 > 85 && Randombackground3 <= 99)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[2] = 2;
            }
            else if (Randombackground3 == 100)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[2] = 3;
            }
            // 4번째 카드 등급
            if (Randombackground4 <= 40)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[3] = 0;
            }
            else if (Randombackground4 > 40 && Randombackground4 <= 85)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[3] = 1;
            }
            else if (Randombackground4 > 85 && Randombackground4 <= 99)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[3] = 2;
            }
            else if (Randombackground4 == 100)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[3] = 3;
            }
        }
        else if (GameManger.GetComponent<InforMation>().Stage > 20 && GameManger.GetComponent<InforMation>().Stage <= 25) // 라운드 21 ~ 25 일때 희귀(회색) 30%, 영웅(보라) 40%, 전설(노랑) 20%, 고대(파랑) 10%
        {
            //1번째 등급
            if (Randombackground1 <= 30)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[0] = 0;
            }
            else if (Randombackground1 > 30 && Randombackground1 <= 70)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[0] = 1;
            }
            else if (Randombackground1 > 70 && Randombackground1 <= 90)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[0] = 2;
            }
            else if (Randombackground1 > 90 && Randombackground1 <= 100)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[0] = 3;
            }
            //2번째 등급
            if (Randombackground2 <= 30)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[1] = 0;
            }
            else if (Randombackground2 > 30 && Randombackground2 <= 70)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[1] = 1;
            }
            else if (Randombackground2 > 70 && Randombackground2 <= 90)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[1] = 2;
            }
            else if (Randombackground2 > 90 && Randombackground2 <= 100)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[1] = 3;
            }
            //3번째 등급
            if (Randombackground3 <= 30)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[2] = 0;
            }
            else if (Randombackground3 > 30 && Randombackground3 <= 70)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[2] = 1;
            }
            else if (Randombackground3 > 70 && Randombackground3 <= 90)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[2] = 2;
            }
            else if (Randombackground3 > 90 && Randombackground3 <= 100)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[2] = 3;
            }
            //4번째 등급
            if (Randombackground4 <= 30)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[3] = 0;
            }
            else if (Randombackground4 > 30 && Randombackground4 <= 70)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[3] = 1;
            }
            else if (Randombackground4 > 70 && Randombackground4 <= 90)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[3] = 2;
            }
            else if (Randombackground4 > 90 && Randombackground4 <= 100)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[3] = 3;
            }
        }
        else if (GameManger.GetComponent<InforMation>().Stage > 25)// 라운드 26 ~ 29 일때 희귀(회색) 15%, 영웅(보라) 40%, 전설(노랑) 30%, 고대(파랑) 15%
        {
            //1번째 등급
            if (Randombackground1 <= 15)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[0] = 0;
            }
            else if (Randombackground1 > 15 && Randombackground1 <= 55)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[0] = 1;
            }
            else if (Randombackground1 > 55 && Randombackground1 <= 85)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[0] = 2;
            }
            else if (Randombackground1 > 85 && Randombackground1 <= 100)
            {
                RandomItemBackgroundObject[0].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[0] = 3;
            }
            //2번째 등급
            if (Randombackground2 <= 15)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[1] = 0;
            }
            else if (Randombackground2 > 15 && Randombackground2 <= 55)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[1] = 1;
            }
            else if (Randombackground2 > 55 && Randombackground2 <= 85)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[1] = 2;
            }
            else if (Randombackground2 > 85 && Randombackground2 <= 100)
            {
                RandomItemBackgroundObject[1].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[1] = 3;
            }
            //3번째 등급
            if (Randombackground3 <= 15)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[2] = 0;
            }
            else if (Randombackground3 > 15 && Randombackground3 <= 55)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[2] = 1;
            }
            else if (Randombackground3 > 55 && Randombackground3 <= 85)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[2] = 2;
            }
            else if (Randombackground3 > 85 && Randombackground3 <= 100)
            {
                RandomItemBackgroundObject[2].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[2] = 3;
            }
            //4번째 등급
            if (Randombackground4 <= 15)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[0];
                RandomWeaponGrade[3] = 0;
            }
            else if (Randombackground4 > 15 && Randombackground4 <= 55)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[1];
                RandomWeaponGrade[3] = 1;
            }
            else if (Randombackground4 > 55 && Randombackground4 <= 85)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[2];
                RandomWeaponGrade[3] = 2;
            }
            else if (Randombackground4 > 85 && Randombackground4 <= 100)
            {
                RandomItemBackgroundObject[3].GetComponent<Image>().sprite = RandomWeaponBackgroundSprite[3];
                RandomWeaponGrade[3] = 3;
            }
        }
    }
    //랜덤 무기 텍스트 변경
    public void WeaponStatetext(int index, int weapon)
    {
        if (weapon == 0)
        {
            RandomItemname[index].text = "Dagger\n" + "Damage : " + GameManger.GetComponent<InforMation>().Daggerdamage[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Daggerspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Daggerprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Daggerprice[RandomWeaponGrade[index]];
        }
        if (weapon == 1)
        {
            RandomItemname[index].text = "Sword\n" + "Damage : " + GameManger.GetComponent<InforMation>().Sworddamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Swordspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Swordprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Swordprice[RandomWeaponGrade[index]];
        }
        if (weapon == 2)
        {
            RandomItemname[index].text = "Spear\n" + "Damage : " + GameManger.GetComponent<InforMation>().Speardamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Spearspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Spearprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Spearprice[RandomWeaponGrade[index]];
        }
        if (weapon == 3)
        {
            RandomItemname[index].text = "Blunt\n" + "Damage : " + GameManger.GetComponent<InforMation>().Bluntdamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Bluntspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Bluntprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Bluntprice[RandomWeaponGrade[index]];
        }
        if (weapon == 4)
        {
            RandomItemname[index].text = "Gun\n" + "Damage : " + GameManger.GetComponent<InforMation>().Pistoldamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Pistolspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Pistolprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Pistolprice[RandomWeaponGrade[index]];
        }
        if (weapon == 5)
        {
            RandomItemname[index].text = "AK47\n" + "Damage : " + GameManger.GetComponent<InforMation>().AKdamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().AKspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().AKprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().AKprice[RandomWeaponGrade[index]];
        }
        if (weapon == 6)
        {
            RandomItemname[index].text = "ShotGun\n" + "Damage : " + GameManger.GetComponent<InforMation>().Shotgundamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Shotgunspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Shotgunprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Shotgunprice[RandomWeaponGrade[index]];
        }
        if (weapon == 7)
        {
            RandomItemname[index].text = "Sinper\n" + "Damage : " + GameManger.GetComponent<InforMation>().Sinperdamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Sinperspeed[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Sinperprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Sinperprice[RandomWeaponGrade[index]];
        }
        if (weapon == 8)
        {
            RandomItemname[index].text = "Meteor\n" + "Damage : " + GameManger.GetComponent<InforMation>().Firedamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Firedamge[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Fireprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Fireprice[RandomWeaponGrade[index]];
        }
        if (weapon == 9)
        {
            RandomItemname[index].text = "Thunder\n" + "Damage : " + GameManger.GetComponent<InforMation>().Thunderdamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Thunderdamge[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Thunderprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Thunderprice[RandomWeaponGrade[index]];
        }
        if (weapon == 10)
        {
            RandomItemname[index].text = "Void\n" + "Damage : " + GameManger.GetComponent<InforMation>().Voiddamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Voiddamge[RandomWeaponGrade[index]].ToString();
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Voidprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Voidprice[RandomWeaponGrade[index]];
        }
        if (weapon == 11)
        {
            RandomItemname[index].text = "Holy\n" + "Damage : " + GameManger.GetComponent<InforMation>().Holydamge[RandomWeaponGrade[index]].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().Holydamge[RandomWeaponGrade[index]].ToString() + "\n" + "데미지 만큼 HP회복";
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Holyprice[RandomWeaponGrade[index]].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Holyprice[RandomWeaponGrade[index]];
        }
    }
    
    //랜덤 아이템 텍스트 변경
    public void ItemStatetext(int index, int Item)
    {
        if (Item == 0)
        {
            RandomItemname[index].text = "Max HP UP Potion\n" + "플레이어 최대 HP\n + 5";
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Itemprice[Item].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Itemprice[Item];
        }
        else if (Item == 1)
        {
            RandomItemname[index].text = "Damage UP Potion\n" + "모든 무기의 데미지\n + 5";
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Itemprice[Item].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Itemprice[Item];
        }
        else if (Item == 2)
        {
            RandomItemname[index].text = "Attack Speed UP Potion\n" + "모든 무기의 공격 속도\n + 0.25";
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Itemprice[Item].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Itemprice[Item];
        }
        else if (Item == 3)
        {
            RandomItemname[index].text = "Speed UP Potion\n" + "플레이어 이동 속도\n + 0.25";
            BuyButtonprice[index].text = GameManger.GetComponent<InforMation>().Itemprice[Item].ToString();
            RandomItemprice[index] = GameManger.GetComponent<InforMation>().Itemprice[Item];
        }
        else if (Item == 4)
        {
            RandomItemname[index].text = "Small Bouns Coin\n" + "코인 + 10";
            BuyButtonprice[index].text = "무료";
            RandomItemprice[index] = 0;
        }
        else if (Item == 5)
        {
            RandomItemname[index].text = "Big Bouns Coin\n" + "코인 + 25";
            BuyButtonprice[index].text = "무료";
            RandomItemprice[index] = 0;
        }
    }
    //무기등급
    public void WeaponGrade(int a)
    {
        if (RandomItemBackgroundObject[a].GetComponent<Image>().sprite == RandomWeaponBackgroundSprite[0])
        {
            Grade = 0;
        }
        else if (RandomItemBackgroundObject[a].GetComponent<Image>().sprite == RandomWeaponBackgroundSprite[1])
        {
            Grade = 1;
        }
        else if (RandomItemBackgroundObject[a].GetComponent<Image>().sprite == RandomWeaponBackgroundSprite[2])
        {
            Grade = 2;
        }
        else if (RandomItemBackgroundObject[a].GetComponent<Image>().sprite == RandomWeaponBackgroundSprite[3])
        {
            Grade = 3;
        }
    }

    //무기 스텟 적용
    public void WeaponStateSetting(int index, int grade, int i)
    {
        if (Weaponindex[index] == 0)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Daggerdamage[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Daggerspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Daggerprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 0;
        }
        if (Weaponindex[index] == 1)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Sworddamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Swordspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Swordprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 1;
        }
        if (Weaponindex[index] == 2)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Speardamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Spearspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Spearprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 2;
        }
        if (Weaponindex[index] == 3)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Bluntdamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Bluntspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Bluntprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 3;
        }
        if (Weaponindex[index] == 4)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Pistoldamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Pistolspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Pistolprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 4;
        }
        if (Weaponindex[index] == 5)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().AKdamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().AKspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().AKprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 5;
        }
        if (Weaponindex[index] == 6)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Shotgundamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Shotgunspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Shotgunprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 6;
        }
        if (Weaponindex[index] == 7)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Sinperdamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Sinperspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Sinperprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 7;
        }
        if (Weaponindex[index] == 8)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Firedamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Firespeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Fireprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 8;
        }
        if (Weaponindex[index] == 9)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Thunderdamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Thunderspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Thunderprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 9;
        }
        if (Weaponindex[index] == 10)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Voiddamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Voidspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Voidprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 10;
        }
        if (Weaponindex[index] == 11)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] += 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[i] += GameManger.GetComponent<InforMation>().Holydamge[grade];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[i] += GameManger.GetComponent<InforMation>().Holyspeed[grade];
            GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = GameManger.GetComponent<InforMation>().Holyprice[grade];
            GameManger.GetComponent<InforMation>().weaponname[i] = 11;
        }
    }

    //아이템 스텟 적용
    public void ItemStateSetting(int index)
    {
        for(int i = 0; i < 4; i++)
        {
            if(WearItemObject[index].GetComponent<Image>().sprite.name == RandomItemSprite[i].name)
            {
                if (i == 0)
                {
                    GameManger.GetComponent<InforMation>().PlayerHP += 5;
                    GameManger.GetComponent<InforMation>().PlayerMaxHP += 5;
                }
                else if (i == 1) for (int j = 0; j < 6; j++) GameManger.GetComponent<InforMation>().WearWeapondamage[j] += 5;
                else if (i == 2) for (int j = 0; j < 6; j++) GameManger.GetComponent<InforMation>().WearWeaponspeed[j] += 0.25f;
                else if (i == 3) GameManger.GetComponent<InforMation>().PlayerSpeed += 0.25f;
                Itemnum[i]++;
                break;
            }
        }
    }

    //구매 버튼 : 구매버튼 모두 아직 합치는 코드 구현 해야함, 코인 갯수 줄이고 텍스트 변경, 전체무기에서 찾아서 장착중인 무기로 옮김, 무기와 아이템 판단
    public void BuyItem()
    {
        int Buttonnum = 0;
        int count = 0; // 같은 등급이 있는지 확인을 하기 위한 변수
        if (EventSystem.current.currentSelectedGameObject.name == "BUYButton1") Buttonnum = 0;
        else if (EventSystem.current.currentSelectedGameObject.name == "BUYButton2") Buttonnum = 1;
        else if (EventSystem.current.currentSelectedGameObject.name == "BUYButton3") Buttonnum = 2;
        else if (EventSystem.current.currentSelectedGameObject.name == "BUYButton4") Buttonnum = 3;
        if(GameManger.GetComponent<InforMation>().Coin >= RandomItemprice[Buttonnum])
        {
            if (WeaponOrItem[Buttonnum] == "Weapon")
            {
                WeaponGrade(Buttonnum);
                for (int i = 0; i < 6; i++)
                {
                    if (WearWeaponObject[i].GetComponent<Image>().sprite == null)
                    {
                        GameManger.GetComponent<InforMation>().Coin -= RandomItemprice[Buttonnum];
                        cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
                        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
                        WearWeaponObject[i].GetComponent<Image>().sprite = RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite;
                        color = WearWeaponObject[i].GetComponent<Image>().color;
                        color.a = 1f;
                        WearWeaponObject[i].GetComponent<Image>().color = color;
                        GameManger.GetComponent<InforMation>().PlayerWeapon[i] = RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite;
                        WearWeapon[i] = RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite;
                        GameManger.GetComponent<InforMation>().WeaponGrade[i] = Grade;
                        WearWeaponFrameObject[i].GetComponent<Image>().sprite = WearWeaponFrame[Grade];
                        WeaponStateSetting(Buttonnum, GameManger.GetComponent<InforMation>().WeaponGrade[i], i);
                        break;
                    }
                    else count++;
                }
                if (count == 6)
                {
                    // 같은 등급이 있는지 찾고, 합치기
                    for (int i = 0; i < 6; i++)
                    {
                        if (WearWeaponObject[i].GetComponent<Image>().sprite == RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite && GameManger.GetComponent<InforMation>().WeaponGrade[i] == Grade && GameManger.GetComponent<InforMation>().WeaponGrade[i] < 3)
                        {
                            GameManger.GetComponent<InforMation>().Coin -= RandomItemprice[Buttonnum];
                            cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
                            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
                            GameManger.GetComponent<InforMation>().WeaponGrade[i] += 1;
                            WearWeaponFrameObject[i].GetComponent<Image>().sprite = WearWeaponFrame[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                            WeaponStateSetting(Buttonnum, GameManger.GetComponent<InforMation>().WeaponGrade[i],i);
                            break;
                        }
                    }
                }
            }
            else if (WeaponOrItem[Buttonnum] == "Item")
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
                if (RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite.name == "#1 - Transparent Icons_181")
                {
                    GameManger.GetComponent<InforMation>().Coin += 10;
                    cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
                }
                else if (RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite.name == "#1 - Transparent Icons_184")
                {
                    GameManger.GetComponent<InforMation>().Coin += 25;
                    cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
                }
                else
                {
                    GameManger.GetComponent<InforMation>().Coin -= RandomItemprice[Buttonnum];
                    cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
                    for (int i = 0; i < 4; i++)
                    {
                        if (WearItemObject[i].GetComponent<Image>().sprite == null)
                        {
                            WearItemObject[i].GetComponent<Image>().sprite = RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite;
                            color = WearItemObject[i].GetComponent<Image>().color;
                            color.a = 1f;
                            WearItemObject[i].GetComponent<Image>().color = color;
                            ItemStateSetting(i);
                            break;
                        }
                        else if (WearItemObject[i].GetComponent<Image>().sprite == RandomItemImageObject[Buttonnum].GetComponent<Image>().sprite)
                        {
                            ItemStateSetting(i);
                            break;
                        }
                    }
                }
            }
        }
    }
    //버튼 활성화를 위한 함수
    public void Buttonactivate()
    {
        GameObject[] t = GameObject.FindGameObjectsWithTag("BuyButton");
        for (int i = 0; i < 4; i++) t[i].GetComponent<Button>().interactable = true;
    }

    //판매 버튼
    public void SellButton()
    {
        GameManger.GetComponent<InforMation>().Coin += GameManger.GetComponent<InforMation>().WearWeaponPrice[num];
        if (GameManger.GetComponent<InforMation>().weaponname[num] == 0)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Daggerdamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Daggerspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 1)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Sworddamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Swordspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 2)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Speardamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Spearspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 3)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Bluntdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Bluntspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 4)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Pistoldamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Pistolspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 5)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().AKdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().AKspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 6)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Shotgundamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Shotgunspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 7)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Sinperdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Sinperspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 8)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Firedamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Firespeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 9)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Thunderdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Thunderspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 10)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Voiddamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Voidspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        else if (GameManger.GetComponent<InforMation>().weaponname[num] == 11)
        {
            if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]] -= 10;
            GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Holydamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
            GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Holyspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
        }
        color = WearWeaponObject[num].GetComponent<Image>().color;
        color.a = 0f;
        WearWeaponObject[num].GetComponent<Image>().color = color;
        GameManger.GetComponent<InforMation>().WeaponGrade[num] = 0;
        GameManger.GetComponent<InforMation>().weaponname[num] = 12;
        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = 0;
        cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
        WeaponPopUp.SetActive(false);
        GameManger.GetComponent<InforMation>().PlayerWeapon[num] = null;
        WearWeaponObject[num].GetComponent<Image>().sprite = null;
        WearWeaponFrameObject[num].GetComponent<Image>().sprite = WearWeaponFrame[0];
    }

    //합성 버튼
    public void Weaponedit()
    {
        for(int i = 0; i < 6; i++)
        {
            if(i != num)
            {
                if(WearWeaponObject[i].GetComponent<Image>().sprite == WearWeaponObject[num].GetComponent<Image>().sprite && GameManger.GetComponent<InforMation>().WeaponGrade[num] < 3)
                {
                    WearWeaponObject[i].GetComponent<Image>().sprite = null;
                    GameManger.GetComponent<InforMation>().PlayerWeapon[i] = null;
                    WearWeaponFrameObject[i].GetComponent<Image>().sprite = WearWeaponFrame[0];
                    color = WearWeaponObject[i].GetComponent<Image>().color;
                    color.a = 0f;
                    WearWeaponObject[i].GetComponent<Image>().color = color;
                    if (GameManger.GetComponent<InforMation>().weaponname[num] == 0)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Daggerdamage[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Daggerspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Daggerdamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Daggerspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Daggerdamage[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Daggerspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Daggerprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 1)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Sworddamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Swordspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Sworddamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Swordspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Sworddamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Swordspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Swordprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 2)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Speardamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Spearspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Speardamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Spearspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Speardamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Spearspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Spearprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 3)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 0) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Bluntdamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Bluntspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Bluntdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Bluntspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Bluntdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Bluntspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Bluntprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 4)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Pistoldamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Pistolspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Pistoldamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Pistolspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Pistoldamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Pistolspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Pistolprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 5)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().AKdamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().AKspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().AKdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().AKspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().AKdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().AKspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().AKprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 6)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Shotgundamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Shotgunspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Shotgundamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Shotgunspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Shotgundamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Shotgunspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Shotgunprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 7)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 1) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Sinperdamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Sinperspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Sinperdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Sinperspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Sinperdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Sinperspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Sinperprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 8)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Firedamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Firespeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Firedamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Firespeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Firedamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Firespeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Fireprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 9)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Thunderdamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Thunderspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Thunderdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Thunderspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Thunderdamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Thunderspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Thunderprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 10)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Voiddamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Voidspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Voiddamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Voidspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Voiddamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Voidspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Voidprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    else if (GameManger.GetComponent<InforMation>().weaponname[num] == 11)
                    {
                        if (GameManger.GetComponent<InforMation>().PlayerJob == 2) GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= 10;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[i] -= GameManger.GetComponent<InforMation>().Holydamge[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[i] -= GameManger.GetComponent<InforMation>().Holyspeed[GameManger.GetComponent<InforMation>().WeaponGrade[i]];
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] -= GameManger.GetComponent<InforMation>().Holydamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] -= GameManger.GetComponent<InforMation>().Holyspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WeaponGrade[num] += 1;
                        GameManger.GetComponent<InforMation>().WearWeapondamage[num] += GameManger.GetComponent<InforMation>().Holydamge[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponspeed[num] += GameManger.GetComponent<InforMation>().Holyspeed[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                        GameManger.GetComponent<InforMation>().WearWeaponPrice[num] = GameManger.GetComponent<InforMation>().Holyprice[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    }
                    WearWeaponFrameObject[num].GetComponent<Image>().sprite = WearWeaponFrame[GameManger.GetComponent<InforMation>().WeaponGrade[num]];
                    GameManger.GetComponent<InforMation>().WeaponGrade[i] = 0;
                    GameManger.GetComponent<InforMation>().WearWeaponPrice[i] = 0;
                    GameManger.GetComponent<InforMation>().weaponname[i] = 12;
                    WeaponPopUp.SetActive(false);
                    break;
                }
            }
        }
    }

    // 리롤 버튼
    public void ResetButton()
    {
        if(GameManger.GetComponent<InforMation>().Coin >= GameManger.GetComponent<InforMation>().ResetCoin)
        {
            Buttonactivate(); // 구매버튼 활성화
            GameManger.GetComponent<InforMation>().Coin -= GameManger.GetComponent<InforMation>().ResetCoin;
            GameManger.GetComponent<InforMation>().ResetCoin += Mathf.CeilToInt(GameManger.GetComponent<InforMation>().ResetCoin * 0.5f);
            cointext.text = GameManger.GetComponent<InforMation>().Coin.ToString();
            Resetcointext.text = "Reset : " + GameManger.GetComponent<InforMation>().ResetCoin.ToString();

            // 라운드별 등급 확률 
            WeaponBackGroundRandom();
            // 아이템은 총 18가지
            ItemRandom();
        }
    }

    //다음 스테이지 버튼
    public void NextStage()
    {
        GameManger.GetComponent<InforMation>().Stage += 1;
        SceneManager.LoadScene("Stage");
        GameManger.GetComponent<Manager>().poolManager.SetActive(true);
    }

    //일시정지 버튼
    public void Pause()
    {
        PausePopUp.SetActive(true);
    }

    // 무기가 장착 되어있다면 Text랑 가격을 바꿔서 보여주는 코드 필요(팝업 버튼)
    public void wearweapon()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Weapon1Button") num = 0;
        else if (EventSystem.current.currentSelectedGameObject.name == "Weapon2Button") num = 1;
        else if (EventSystem.current.currentSelectedGameObject.name == "Weapon3Button") num = 2;
        else if (EventSystem.current.currentSelectedGameObject.name == "Weapon4Button") num = 3;
        else if (EventSystem.current.currentSelectedGameObject.name == "Weapon5Button") num = 4;
        else if (EventSystem.current.currentSelectedGameObject.name == "Weapon6Button") num = 5;
        if (WearWeaponObject[num].GetComponent<Image>().sprite != null)
        {
            WeaponPopUp.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = WearWeaponObject[num].GetComponent<Image>().sprite;
            WeaponPopUp.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = WearWeaponObject[num].GetComponent<Image>().sprite.name;
            WeaponPopUp.gameObject.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "+" + GameManger.GetComponent<InforMation>().WearWeaponPrice[num].ToString();
            if (GameManger.GetComponent<InforMation>().WeaponGrade[num] == 0)
            {
                WeaponPopUp.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Damage : " + GameManger.GetComponent<InforMation>().WearWeapondamage[num].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().WearWeaponspeed[num].ToString() + "\n등급 : 희귀";
            }
            else if (GameManger.GetComponent<InforMation>().WeaponGrade[num] == 1)
            {
                WeaponPopUp.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Damage : " + GameManger.GetComponent<InforMation>().WearWeapondamage[num].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().WearWeaponspeed[num].ToString() + "\n등급 : 영웅";
            }
            else if (GameManger.GetComponent<InforMation>().WeaponGrade[num] == 2)
            {
                WeaponPopUp.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Damage : " + GameManger.GetComponent<InforMation>().WearWeapondamage[num].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().WearWeaponspeed[num].ToString() + "\n등급 : 전설";
            }
            else if (GameManger.GetComponent<InforMation>().WeaponGrade[num] == 3)
            {
                WeaponPopUp.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Damage : " + GameManger.GetComponent<InforMation>().WearWeapondamage[num].ToString() + "\n" + "Attack Speed : " + GameManger.GetComponent<InforMation>().WearWeaponspeed[num].ToString() + "\n등급 : 특급";
            }
            WeaponPopUp.SetActive(true);
        }
    }
    //팝업 종료
    public void PopUpCancel()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "PausePopupCancel")
        {
            PausePopUp.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "WeaponPopupCancelButton")
        {
            WeaponPopUp.SetActive(false);
            WeaponPopUp.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = null;
        }
    }

    //메인 메뉴로 돌아가는 버튼, 메인 메뉴로 씬 이동
    public void MainMenu()
    {
        Time.timeScale = 1;
        Destroy(GameManger);
        Destroy(pool);
        SceneManager.LoadScene("MainMenu");
    }

    //세팅 팝업 열기, 소리 조정
    public void SettingPopup()
    {

    }
}
