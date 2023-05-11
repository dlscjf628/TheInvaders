using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, GunDamage
{
    float speed = 5f;
    Rigidbody2D rigid;
    Animator ani;
    GameObject unit;
    GameObject Ak47;
    public GameObject HE;//수류탄
    public GameObject heLocation; //수류탄 나가는 위치
    public GameObject map;
    public GameObject recovery;
    public GameObject permanent;
    //public GameObject Inventory;
    public Image hpBar;
    public Image hpIcon;
    public Text hpText;

    Weapon ak;
    Gun gun;
    ShotGun shotGun;
    Sniper sniper;
    Grenade grenade;
    Recovery potion;
    Permanent perPotion;

    public float hpMax = 10000f;
    public float hp = 10000f;
    float t = 3f;
    bool b = false;
    int cnt;
    bool m;

    public bool slow, poison;

    float t2 = 0f;//수류탄 딜레이

    Vector2 mouse;
    PlayerInput playInput;
    PlayerWeapon playerWeapon;

    // Start is called before the first frame update
    void Start()
    {
        unit = transform.GetChild(0).gameObject;
        Ak47 = transform.GetChild(1).gameObject;
        rigid = GetComponent<Rigidbody2D>();
        ani = unit.GetComponent<Animator>();
        playInput = GetComponent<PlayerInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
        ak = Ak47.GetComponent<Weapon>();
        shotGun = transform.GetChild(3).gameObject.GetComponent<ShotGun>();
        gun = transform.GetChild(4).gameObject.GetComponent<Gun>();
        sniper= transform.GetChild(5).gameObject.GetComponent<Sniper>();
        grenade = HE.GetComponent<Grenade>();
        potion = recovery.GetComponent<Recovery>();
        perPotion = permanent.GetComponent<Permanent>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Manager.instance != null && Manager.instance.gameOver))
        {
            Move();
            Angle();
            PlayerInput();
            Roll2();
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    if (Inventory.activeSelf == true)
        //    {
        //        Inventory.SetActive(false);
        //    }
        //    else if (Inventory.activeSelf == false)
        //    {
        //        Inventory.SetActive(true);
        //    }
        //}
        if (slow)
        {
            slow = false;
            StartCoroutine(Slow());
        }
        if (poison)
        {
            poison = false;
            StartCoroutine(Poison(5));
        }
        hpBarText();
    }

    //캐릭터 움직임
    void Move()
    {
        t += Time.deltaTime;
        t2 += Time.deltaTime;
        Vector2 move = new Vector2(playInput.move1, playInput.move2) * speed;
        rigid.velocity = move;
       

        if (!(rigid.velocity.x == 0 && rigid.velocity.y == 0) && b == false)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
      
    }

    void Angle()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (direction.x > transform.position.x)
            unit.transform.localScale = new Vector3(-1, 1, 1);
        if (direction.x <= transform.position.x)
            unit.transform.localScale = new Vector3(1, 1, 1);
    }

    void PlayerInput()
    {
        if (playInput.shot)  //좌클릭을 해 공격했을 경우
        {
            if (playerWeapon.weaponeNum == 1)
            {
                ak.Shot();
                shotGun.Shot();
                gun.Shot();
                sniper.Shot();
            }
            else if (playerWeapon.weaponeNum == 2)
            {

            }
            else if (playerWeapon.weaponeNum == 3)
            {
                if (t2 > 3f && playerWeapon.recoveryCnt > 0)
                {
                    float t = hp + potion.recovery;
                    if (t > hpMax)
                        t = hpMax;
                    hp = t;
                    playerWeapon.recoveryCnt--;
                    StartCoroutine(WeaponeSwitching());
                    print(hp);
                    t2 = 0;
                }
            }
            else if (playerWeapon.weaponeNum == 4)
            {
                if (t2 > 3f && playerWeapon.permanentCnt > 0)
                {
                    hpMax += perPotion.health;
                    print(hpMax);
                    playerWeapon.permanentCnt--;
                    StartCoroutine(WeaponeSwitching());
                    t2 = 0;
                }
            }
            else if (playerWeapon.weaponeNum == 5)
            {
                if (t2 > 3f && playerWeapon.grenadeCnt > 0)
                {
                    Instantiate(HE, heLocation.transform.position, Quaternion.identity);
                    playerWeapon.grenadeCnt--;
                    StartCoroutine(WeaponeSwitching());
                    t2 = 0;
                }
            }
        }
        else if (playInput.reload) //r키를 눌러 장전 했을 경운
        {
            if (playerWeapon.weaponeNum == 1)
            {
                ak.Reload();
                shotGun.Reload();
                gun.Reload();
                sniper.Reload();
            }
            else if (playerWeapon.weaponeNum == 2)
            {

            }
            else if (playerWeapon.weaponeNum == 3)
            {

            }
            else if (playerWeapon.weaponeNum == 4)
            {

            }
        }
        else if (playInput.Space && t > 3f)
        {
            Roll();
        }
        else if (playInput.tap)
        {
            if (!m)
            {
                map.SetActive(true);
                m = true;
            }
            else
            {
                map.SetActive(false);
                m = false;
            }
        }
        else //총을 쏘지 않으면 불 이펙트가 꺼짐
        {
            ak.mouseUI.SetActive(false);
        }
    }

    //구르기 이펙트
    void Roll()
    {
        t = 0;
        b = true;

        if (unit.transform.localScale.x > 0)
        {
            ani.SetBool("Roll", true);
            StartCoroutine(stop());
        }
        else if (unit.transform.localScale.x == -1)
        {
            ani.SetBool("Roll2", true);
            StartCoroutine(stop2());
        }

    }

    //멈춰있을때 구르기를 할때
    void Roll2()
    {
        if (b)
        {

            speed = 10f;

            if (rigid.velocity == Vector2.zero)
            {
                if (cnt == 0)
                {
                    mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    cnt++;
                }
                rigid.velocity = mouse.normalized * speed;
            }

            StartCoroutine(stop3());
        }
    }

    public virtual void OnDamage(float damage)
    {
        hp -= damage;
        print(hp);
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Manager.instance.gameOver = true;
        ani.SetTrigger("Die");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

    }

    void hpBarText()
    {
        hpBar.fillAmount = hp / hpMax;
        hpText.text = hpMax.ToString() + "/" + hp.ToString();
    }

    IEnumerator stop()
    {

        yield return new WaitForSeconds(0.4f);
        ani.SetBool("Roll", false);
        
    }

    IEnumerator stop2()
    {

        yield return new WaitForSeconds(0.4f);
        ani.SetBool("Roll2", false);

    }

    IEnumerator stop3()
    {
        yield return new WaitForSeconds(0.5f);
        cnt = 0;
        speed = 5f;
        b = false;
    }
    IEnumerator WeaponeSwitching()
    {
        yield return new WaitForSeconds(0.5f);
        playInput.change = 1;
    }
    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(1f);
        Manager.instance.PlayerDie();
    }
    IEnumerator Slow()
    {
        speed = 3f;
        yield return new WaitForSeconds(3f);
        speed = 5f;
    }
    IEnumerator Poison(int a)
    {
        hpIcon.color = new Color(0, 1, 0, 1);
        hpBar.color = new Color(0, 1, 0, 1);
        hp -= 1f;
        yield return new WaitForSeconds(1f);
        if (a != 0)
        {
            StartCoroutine(Poison(--a));
        }
        else
        {
            hpIcon.color = new Color(1, 1, 1, 1);
            hpBar.color = new Color(1, 1, 1, 1);
        }
    }
}
