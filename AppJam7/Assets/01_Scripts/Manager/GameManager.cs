using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int curStage = 1;
    public static int painGauge; // 최대 100

    public PlayerController player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<PlayerController>();

        print(curStage);
    }

    public void TakePain(int value)
    {
        if (painGauge + value < 100)
        {
            painGauge += value;
        }
        else
        {
            painGauge = 100;
        }
    }

    public void TakeHeal(int value)
    {
        if (painGauge - value > 0)
        {
            painGauge -= value;
        }
        else
        {
            painGauge = 0;
        }
    }

    public void NextStage()
    {
        UIManager.instance.SetText("");
        UIManager.instance.FadeIn(1);

        switch (curStage)
        {
            case 1:
                curStage = 2;
                StartCoroutine(DelayTime(1f, "Stage2"));
                break;
            case 2:
                curStage = 3;
                StartCoroutine(DelayTime(1f, "Stage3"));
                break;
            case 3:
                Clear();
                break;
        }
    }

    public void GameOver()
    {
        UIManager.instance.FadeIn(1);
        UIManager.instance.SetText("게임오버");
        StartCoroutine(DelayTime(2f, SceneManager.GetActiveScene().name));
    }

    private IEnumerator DelayTime(float time, string name)
    {
        yield return new WaitForSeconds(time);
        DOTween.KillAll();
        SceneManager.LoadScene(name);
    }

    public void Clear()
    {
        if (painGauge < 20)
        {
            SceneManager.LoadScene("HappyEnding");
        }
        else
        {
            SceneManager.LoadScene("DieEnding");
        }
    }
}
