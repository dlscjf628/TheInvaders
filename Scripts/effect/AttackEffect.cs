using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    SpriteRenderer sp;
    public float FadeSpeed = 1f; // »ç¶óÁü ¼Óµµ
    public float destroy = 0.25f; // ÆÄ±«½ÃÁ¡
    float color;

    AudioSource hitSound;

    public InforMation info;
    private void Start()
    {
        info = info = GameObject.Find("GameManager").GetComponent<InforMation>();
        hitSound = GetComponent<AudioSource>();
        hitSound.volume = info.EffectSoundValue / 100;
        sp = gameObject.GetComponent<SpriteRenderer>();
        hitSound.PlayOneShot(hitSound.clip);
    }
    private void OnEnable()
    {
        if (info != null)
        {
            hitSound.volume = info.EffectSoundValue / 100;
            hitSound.PlayOneShot(hitSound.clip);
        }

    }
    // Update is called once per frame
    void Update()
    {
        float currentAlpha = sp.color.a;
        currentAlpha -= FadeSpeed * Time.deltaTime;

        Color newColor = sp.color;
        newColor.a = currentAlpha;
        sp.color = newColor;

        if (currentAlpha <= destroy)
        {
            sp.color = new Color(1f, 1f, 1f, 1f);
            gameObject.SetActive(false);
        }
    }
}
