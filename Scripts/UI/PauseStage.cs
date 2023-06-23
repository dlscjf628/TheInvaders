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
    //세팅 버튼에서 돌아가기 버튼에도 넣어놨음
    //그래서 세팅 버튼 false도 넣어둔거임.
    public void PauseGame()  
    {
        ButtonClick.PlayOneShot(ButtonClick.clip);
        Time.timeScale = 0;
        PauseSetting.SetActive(false);// 세팅버튼 false
        PausePopUp.SetActive(true); // 정지 버튼 true

    }

    public void StartGame()
    {
        ButtonClick.PlayOneShot(ButtonClick.clip);
        Time.timeScale = 1;

        PausePopUp.SetActive(false);// 정지버튼 false
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
