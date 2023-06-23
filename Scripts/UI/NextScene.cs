using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public GameObject PlaySelect;
    public GameObject MainCanvas;
    public GameObject manager;
    public GameObject poolManager;
    public GameObject Setting;
    public Sprite[] StartWeapon = new Sprite[4];
    public GameObject[] SelectCheck = new GameObject[4];
    InforMation info;
    int num = 4;

    AudioSource ButtonClick;

    private void Start()
    {
        manager = GameObject.Find("GameManager");
        if (!poolManager)
        {
            poolManager = GameObject.Find("PoolManager");
        }
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        ButtonClick = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (ButtonClick)
        {
            ButtonClick.volume = info.EffectSoundValue / 100;
        }
    }
    public void SwitchToPlayer()
    {
        MainCanvas.SetActive(false);
        ButtonClick.PlayOneShot(ButtonClick.clip);
        PlaySelect.SetActive(true);
    }
    public void SettingCanvas()
    {
        MainCanvas.SetActive(false);
        ButtonClick.PlayOneShot(ButtonClick.clip);
        Setting.SetActive(true);
    }
    public void SetMainCanvas()
    {
        Setting.SetActive(false);
        ButtonClick.PlayOneShot(ButtonClick.clip);
        MainCanvas.SetActive(true);
    }
    public void SelectMainCanvas()
    {
        PlaySelect.SetActive(false);
        ButtonClick.PlayOneShot(ButtonClick.clip);
        MainCanvas.SetActive(true);
        num = 4;
    }
    public void LoadWarriorNextScene()
    {
        info.PlayerJob = 0;
        info.weaponname[0] = 0;
        info.PlayerWeapon[0] = StartWeapon[0];
        info.WearWeapondamage[0] = info.Daggerdamage[0] + 10;
        info.WearWeaponspeed[0] = info.Daggerspeed[0];
        info.WearWeaponPrice[0] = info.Daggerprice[0];
        ButtonClick.PlayOneShot(ButtonClick.clip);
        SceneManager.LoadScene("Stage");
        poolManager.SetActive(true);
    }
    public void LoadGunnerNextScene()
    {
        info.PlayerJob = 1;
        info.weaponname[0] = 4;
        info.PlayerWeapon[0] = StartWeapon[1];
        info.WearWeapondamage[0] = info.Pistoldamge[0] + 10;
        info.WearWeaponspeed[0] = info.Pistolspeed[0];
        info.WearWeaponPrice[0] = info.Pistolprice[0];
        ButtonClick.PlayOneShot(ButtonClick.clip);
        SceneManager.LoadScene("Stage");
        poolManager.SetActive(true);
    }
    public void LoadMageNextScene()
    {
        info.PlayerJob = 2;
        info.weaponname[0] = 8;
        info.PlayerWeapon[0] = StartWeapon[2];
        info.WearWeapondamage[0] = info.Firedamge[0] + 10;
        info.WearWeaponspeed[0] = info.Firespeed[0];
        info.WearWeaponPrice[0] = info.Fireprice[0];
        ButtonClick.PlayOneShot(ButtonClick.clip);
        SceneManager.LoadScene("Stage");
        poolManager.SetActive(true);
    }
    public void LoadGamblerNextScene()
    {
        info.PlayerJob = 3;
        info.weaponname[0] = 1;
        info.PlayerWeapon[0] = StartWeapon[3];
        info.Coin += 50;
        info.WearWeapondamage[0] = info.Sworddamge[0];
        info.WearWeaponspeed[0] = info.Swordspeed[0];
        info.WearWeaponPrice[0] = info.Swordprice[0];
        SceneManager.LoadScene("Stage");
        poolManager.SetActive(true);
    }
    public void LoadMainScene()
    {
        Time.timeScale = 1;
        Destroy(manager);
        Destroy(poolManager);
        SceneManager.LoadScene("MainMenu");
        ButtonClick.PlayOneShot(ButtonClick.clip);
    }
    public void SelectCheckSetActive()
    {
        ButtonClick.PlayOneShot(ButtonClick.clip);
        if (EventSystem.current.currentSelectedGameObject.name == "WarriorSelect") num = 0;
        else if (EventSystem.current.currentSelectedGameObject.name == "GunnerSelect") num = 1;
        else if (EventSystem.current.currentSelectedGameObject.name == "MageSelect") num = 2;
        else if (EventSystem.current.currentSelectedGameObject.name == "GamblerSelect") num = 3;
        for(int i = 0; i < 4; i++)
        {
            if (i == num) SelectCheck[i].SetActive(true);
            else SelectCheck[i].SetActive(false);
        }
    }
    public void GameStart()
    {
        ButtonClick.PlayOneShot(ButtonClick.clip);
        if (num == 0) LoadWarriorNextScene();
        else if (num == 1) LoadGunnerNextScene();
        else if (num == 2) LoadMageNextScene();
        else if (num == 3) LoadGamblerNextScene();
        num = 4;
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void MiniSetting()
    {
        manager.transform.GetChild(3).gameObject.SetActive(true);
    }
}
