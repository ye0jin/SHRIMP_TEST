using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int curStage;
    public static int painGauge; // �ִ� 100

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
        switch (curStage)
        {
            case 1:
                curStage = 2;
                SceneManager.LoadScene("Stage2");
                break;
            case 2:
                curStage = 3;
                SceneManager.LoadScene("Stage3");
                break;
            case 3:
                Clear();
                break;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
