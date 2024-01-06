using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObstacleParent : MonoBehaviour
{
    [SerializeField] private bool isDown;
    [SerializeField] private float speed;
    [SerializeField] private int destroyCount;
    [SerializeField] private int pain;
    [SerializeField] private int heal;
    private bool isEnd;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Pain()
    {
        if (!isEnd)
        {
            isEnd = true;
            StartCoroutine(PainCoroutine());
        }
    }

    private IEnumerator PainCoroutine()
    {
        for (int i=0; i<100; i++)
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
            yield return new WaitForSeconds(0.005f);
        }

        GameManager.Instance.TakePain(pain);

        yield break;
    }

    public void Heal()
    {
        if (isEnd) return;

        destroyCount--;

        if (destroyCount == 0 && !isEnd)
        {
            isEnd = true;
            StartCoroutine(HealCoroutine());
        }
    }

    private IEnumerator HealCoroutine()
    {
        if (isDown)
        {
            for (int i = 0; i < 200; i++)
            {
                transform.Translate(Vector2.up * speed * 2 * Time.deltaTime);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.005f);
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            for (int i = 0; i < 200; i++)
            {
                transform.Translate(Vector2.down * speed * 2 * Time.deltaTime);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.005f);
                yield return new WaitForSeconds(0.005f);
            }
        }

        GameManager.Instance.TakeHeal(heal);

        yield break;
    }
}
