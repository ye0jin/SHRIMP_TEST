using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public static Gauge Instance;

    [Header("수치")]
    [SerializeField] private float gaugeFillSpeed = 0.05f;
    private float divideAmount = 100; // 100 기준으로
    private float maxImageScaleY = 1; // 최대 스케일
    private float currentImageScaleY = 1; // 현재 스케일

    [Header("UI")]
    [SerializeField] private Transform gaugeImage;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Gauge Error");
        }

        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(FillDashGauge());
    }

    private void Update() // 디버깅용 (추후 삭제)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseDash(10);
        }
    }

    public IEnumerator FillDashGauge()
    {
        if(currentImageScaleY >= maxImageScaleY) // 현재 게이지 >= 최대 게이지
        {
            //print("대기");
            yield return new WaitUntil(() => currentImageScaleY < maxImageScaleY); // 현재 게이지 < 최대 게이지 될때까지 대기
        }

        currentImageScaleY += Time.deltaTime * gaugeFillSpeed; // 현재 게이지에서 조금씩 증가
        SetDashGauge(currentImageScaleY);
        yield return null;

        StartCoroutine(FillDashGauge());
    }

    // 최대가 100인 기준으로 넣어주세욧 (divideAmount 기준으로)
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
