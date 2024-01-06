using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("ESC")]
    private bool isSetting = false;
    [SerializeField] private Transform settingPanel;

    [Header("ESC - SoundUI")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private string bgmKey = "BGMVolume";
    private string sfxKey = "SFXVolume";

    [SerializeField] private Image fade;
    [SerializeField] private Image dashGauge;
    [SerializeField] private Image painGauge;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FadeIn(0);


        isSetting = false;
        settingPanel.gameObject.SetActive(false);

        float bgmVolume = PlayerPrefs.GetFloat(bgmKey);
        float sfxVolume = PlayerPrefs.GetFloat(sfxKey);

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    private void Update()
    {
        SetUI();
        SetEscPanel();
    }

    public void SetEscPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSetting)
            {
                Time.timeScale = 1;
                isSetting = false;
                settingPanel.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                isSetting = true;
                settingPanel.gameObject.SetActive(true);
            }
        }
    }

    public void BGMSliderChangedVolume()
    {
        float bgmVolume = bgmSlider.value;

        SoundManager.Instance.SetBGMVolume(bgmVolume);
        PlayerPrefs.SetFloat(bgmKey, bgmVolume);
    }

    public void SFXSliderChangedVolume()
    {
        float sfxVolume = sfxSlider.value;

        SoundManager.Instance.SetSFXVolume(sfxVolume);
        PlayerPrefs.SetFloat(sfxKey, sfxVolume);
    }

    public void FadeIn(float value)
    {
        fade.DOFade(value, 1f);
    }

    private void SetUI()
    {
        if (GameManager.Instance.player)
        {
            dashGauge.fillAmount = GameManager.Instance.player.CurDashGauge / 100f;
        }

        if (GameManager.Instance)
        {
            painGauge.fillAmount = GameManager.painGauge / 100f;
        }
    }
}
