using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

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
    }

    public void StartGame()
    {
        GameManager.curStage = 1;
        GameManager.painGauge = 50;
        SceneManager.LoadScene("Stage1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
