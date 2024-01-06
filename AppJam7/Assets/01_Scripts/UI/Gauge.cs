using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    [Header("수치")]
    [SerializeField] private float delayTime = 5f; // 게이지 차는 속도
    private float divideAmount = 100;

    private float maxImageScaleY = 1; // 최대 게이지 (100로 고정)
    private float currentImageScaleY = 1; // 현재 

    [Header("UI")]
    [SerializeField] private Transform gaugeImage;

    private void Start()
    {
        StartCoroutine(FillDashGauge());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("누름");
            UseDash(10);
        }
    }

    public IEnumerator FillDashGauge()
    {
        if(currentImageScaleY >= maxImageScaleY) // 현재 게이지 >= 최대 게이지
        {
            print("대기");
            yield return new WaitUntil(() => currentImageScaleY <= maxImageScaleY); // 대기
        }

        currentImageScaleY += Time.deltaTime * 0.1f; // 현재 게이지에서 조금씩 증가
        SetDashGauge(currentImageScaleY);
        yield return null;

        StartCoroutine(FillDashGauge());
    }

    // 최대가 100인 기준으로 넣어주세욧
    public void UseDash(float amount)
    {
        float decreaseAmount = amount / divideAmount;
       
        if(decreaseAmount > currentImageScaleY)
        {
            print("대쉬 불가 상태");
            return;
        }

        currentImageScaleY -= decreaseAmount;
        SetDashGauge(currentImageScaleY);
    }

    public void SetDashGauge(float amount)
    {
        gaugeImage.localScale = new Vector3(1, amount, 1);
    }
}
