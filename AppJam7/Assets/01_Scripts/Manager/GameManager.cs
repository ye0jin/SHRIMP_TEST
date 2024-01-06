using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int curStage;
    public static int painGauge; // √÷¥Î 100

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

    public void UpPainGauge(int value)
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

    }

    public void Clear()
    {
        if (painGauge < 30)
        {

        }
        else
        {

        }
    }
}
