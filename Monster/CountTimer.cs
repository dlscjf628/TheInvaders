using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountTimer : MonoBehaviour
{
    public Text countdownText;
    public Text StageText;
    float countdownValue = 0;

    GameObject pool;
    GameObject player;
    GameObject manager;
    GameObject joy;
    public InforMation info;

    GameObject[] boss = new GameObject[3];
    GameObject[] boss2 = new GameObject[3];
    public int bossCount;
    int count;
    bool cnt;
    bool b;
    bool a;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        pool = GameObject.Find("PoolManager");
        manager = GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        joy = GameObject.Find("Joystick_Time_Buff");
        InvokeRepeating("UpdateCountdown", 0f, 0.01f);


        bossCount = -1;
        boss[0] = manager.GetComponent<Manager>().boss[0];
        boss2[0] = Instantiate(boss[0], new Vector3(0f, 6f, 0f), Quaternion.identity);

        boss[1] = manager.GetComponent<Manager>().boss[2];
        boss2[1] = Instantiate(boss[1], new Vector3(0f, 6f, 0f), Quaternion.identity);

        boss[2] = manager.GetComponent<Manager>().boss[1];
        boss2[2] = Instantiate(boss[2], new Vector3(0f, 6f, 0f), Quaternion.identity);

        boss2[0].SetActive(false);
        boss2[1].SetActive(false);
        boss2[2].SetActive(false);

        if (info.Stage <= 3) countdownValue = 30f;
        else if (info.Stage <= 6) countdownValue = 40f;
        else if (info.Stage <= 9) countdownValue = 50f;
        else countdownValue = 60f;
        if (info.Stage % 10 == 0)
        {
            countdownValue = 300f;
            if (info.Stage == 10)
            {
                boss2[0].SetActive(true);
                a = true;
                bossCount = 0;
            }
            else if (info.Stage == 20)
            {
                boss2[1].SetActive(true);
                a = true;
                bossCount = 1;
            }
            else if (info.Stage == 30)
            {
                boss2[2].SetActive(true);
                a = true;
                bossCount = 2;
            }
        }
    }

    void UpdateCountdown()
    {
        if (!Manager.instance.gameOver)
        {
            countdownValue -= 0.01f;
            if (countdownValue <= 0)
            {
                countdownValue = 0;
            }
            StageText.text = "Stage " + info.Stage.ToString();
            countdownText.text = countdownValue.ToString("F0");

            if (!b)
            {
                if (info.Stage == 10)
                {
                    if (!boss2[0].transform.GetChild(0).GetComponent<FlyBossAttack>().isLive && bossCount == 0)
                    {
                        b = true;
                        a = false;
                        bossCount++;
                        countdownValue = 5f;
                    }
                }
                else if (info.Stage == 20)
                {
                    if (!boss2[1].transform.GetChild(0).GetComponent<WizardAttack>().isLive && bossCount == 1)
                    {
                        b = true;
                        a = false;
                        bossCount++;
                        countdownValue = 5f;
                    }
                }
                else if (info.Stage == 30)
                {
                    if (!boss2[2].transform.GetChild(0).GetComponent<EvilGodAttack>().isLive && bossCount == 2)
                    {
                        b = true;
                        a = false;
                        bossCount++;
                        countdownValue = 5f;
                    }
                }
            }

            if (countdownValue <= 0f && !cnt && !a)
            {
                //상점넘어가는 씬
                //Manager.instance.level++;
                cnt = true;
                int stage = info.Stage + 1;
                //info.Stage++;
                Manager.instance.level++;
                count = pool.transform.childCount;
                for (int i = 1; i < count; i++)
                {
                    pool.transform.GetChild(i).gameObject.SetActive(false);
                }
                //player.transform.position = new Vector2(0f, 0f);

                if (stage == 31)
                {
                    Destroy(pool);
                    Destroy(manager);
                    SceneManager.LoadScene("Ending");
                }
                else
                {
                    StartCoroutine(Clear());
                    pool.SetActive(false);
                }
            }
            else if(countdownValue <= 0f && a)
            {
                Time.timeScale = 0;
                manager.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    IEnumerator Clear()
    {
        manager.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        manager.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        info.PlayerSpeed = joy.GetComponent<StageBuff>().moveSpeed;
        for (int i = 0; i < 6; i++)
        {
            info.WearWeaponspeed[i] = joy.GetComponent<StageBuff>().attackSpeed[i];
        }
        if ((info.PlayerHP + info.PlayerMaxHP * 0.1f) < info.PlayerMaxHP)
            info.PlayerHP += info.PlayerMaxHP * 0.1f;
        else
            info.PlayerHP = info.PlayerMaxHP;
        pool.SetActive(true);
        pool.transform.GetChild(0).gameObject.GetComponent<Spawner>().cnt = 0;
        SceneManager.LoadScene("ShopGUI");
    }

}