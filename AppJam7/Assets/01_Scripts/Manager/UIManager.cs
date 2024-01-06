using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image dashGauge;
    [SerializeField] private Image painGauge;

    private void Update()
    {
        SetUI();
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
