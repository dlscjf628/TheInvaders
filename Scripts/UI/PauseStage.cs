using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStage : MonoBehaviour
{
    public GameObject Pause;
    public GameObject PausePopUp;
    public GameObject PauseSetting;

    AudioSource ButtonClick;
    InforMation info;
    private void Start()
    {
        ButtonClick = GetComponent<AudioSource>();

        info  = info = GameObject.Find("GameManager").GetComponent<InforMation>();
    }
    private void Update()
    {
        ButtonClick.volume = info.EffectSoundValue / 100;
    }
    //���� ��ư���� ���ư��� ��ư���� �־����
    //�׷��� ���� ��ư false�� �־�а���.
    public void PauseGame()  
    {
        ButtonClick.PlayOneShot(ButtonClick.clip);
        Time.timeScale = 0;
        PauseSetting.SetActive(false);// ���ù�ư false
        PausePopUp.SetActive(true); // ���� ��ư true

    }

    public void StartGame()
    {
        ButtonClick.PlayOneShot(ButtonClick.clip);
        Time.timeScale = 1;

        PausePopUp.SetActive(false);// ������ư false
    }

    public void SettingGame()
    {
        PausePopUp.SetActive(false);  
        ButtonClick.PlayOneShot(ButtonClick.clip);
        PauseSetting.SetActive(true);   
    }
    /*
    private void OnApplicationPause(bool pause)
    {
        
    }*/

}
