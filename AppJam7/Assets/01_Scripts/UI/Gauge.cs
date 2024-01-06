using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public static Gauge Instance;

    [Header("��ġ")]
    [SerializeField] private float gaugeFillSpeed = 0.05f;
    private float divideAmount = 100; // 100 ��������
    private float maxImageScaleY = 1; // �ִ� ������
    private float currentImageScaleY = 1; // ���� ������

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

    private void Update() // ������ (���� ����)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseDash(10);
        }
    }

    public IEnumerator FillDashGauge()
    {
        if(currentImageScaleY >= maxImageScaleY) // ���� ������ >= �ִ� ������
        {
            //print("���");
            yield return new WaitUntil(() => currentImageScaleY < maxImageScaleY); // ���� ������ < �ִ� ������ �ɶ����� ���
        }

        currentImageScaleY += Time.deltaTime * gaugeFillSpeed; // ���� ���������� ���ݾ� ����
        SetDashGauge(currentImageScaleY);
        yield return null;

        StartCoroutine(FillDashGauge());
    }

    // �ִ밡 100�� �������� �־��ּ��� (divideAmount ��������)
    public void UseDash(float amount)
    {
        float decreaseAmount = amount / divideAmount;
       
        if(decreaseAmount > currentImageScaleY)
        {
            print("�뽬 �Ұ� ����");
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
