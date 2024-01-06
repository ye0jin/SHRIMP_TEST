using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    [Header("��ġ")]
    [SerializeField] private float delayTime = 5f; // ������ ���� �ӵ�
    private float divideAmount = 100;

    private float maxImageScaleY = 1; // �ִ� ������ (100�� ����)
    private float currentImageScaleY = 1; // ���� 

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
            print("����");
            UseDash(10);
        }
    }

    public IEnumerator FillDashGauge()
    {
        if(currentImageScaleY >= maxImageScaleY) // ���� ������ >= �ִ� ������
        {
            print("���");
            yield return new WaitUntil(() => currentImageScaleY <= maxImageScaleY); // ���
        }

        currentImageScaleY += Time.deltaTime * 0.1f; // ���� ���������� ���ݾ� ����
        SetDashGauge(currentImageScaleY);
        yield return null;

        StartCoroutine(FillDashGauge());
    }

    // �ִ밡 100�� �������� �־��ּ���
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
