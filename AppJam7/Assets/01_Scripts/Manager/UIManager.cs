using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

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
    }
    private void Update()
    {
        SetUI();
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
