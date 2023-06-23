using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public InforMation info;
    public Slider Mainslider;
    public Slider Effectslider;

    AudioSource MainSound;

    private void Start()
    {
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        MainSound = GetComponent<AudioSource>();
        Mainslider.value = info.MainSoundValue;
        Effectslider.value = info.EffectSoundValue;
        print(Mainslider.value);
        print(Effectslider.value);
    }

    // Update is called once per frame
    void Update()
    {
        info.MainSoundValue = Mainslider.value;
        info.EffectSoundValue = Effectslider.value;

        MainSound.volume = info.MainSoundValue / 100;

    }

}
