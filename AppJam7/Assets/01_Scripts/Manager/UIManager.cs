using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("")]
    [SerializeField] private CanvasGroup fade;
    [SerializeField] private TextMeshProUGUI fadeText;
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
        settingPanel.transform.localPosition = new Vector3(0, 1000f, 0);

        float bgmVolume = PlayerPrefs.GetFloat(bgmKey);
        float sfxVolume = PlayerPrefs.GetFloat(sfxKey);

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        int key = GameManager.curStage;
        switch (key)
        {
            case 1:
                SetText("스테이지 1");
                break;
            case 2:
                SetText("스테이지 2");
                break;
            case 3:
                SetText("스테이지 3");
                break;
            default:
                SetText("");
                break;
        }
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
                Off();
            }
            else
            {
                On();
            }
        }
    }

    public void Off()
    {
        Time.timeScale = 1;
        settingPanel.transform.DOLocalMoveY(1000f, 0.6f);
        isSetting = false;
    }
    public void On()
    {
        settingPanel.transform.DOLocalMoveY(0f,0.8f).OnComplete(() => Time.timeScale = 0);
        isSetting = true;
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
    public void SetText(string text)
    {
        fadeText.text = text;
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

    public void OnClickTitleButton()
    {
        Off();
        fade.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(0));
    }
}
