using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("애니메이션")]
    [SerializeField] private Transform targetTrm;
    [SerializeField] private Transform animShrimp;
    [SerializeField] private Image fadeImage;

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

    private void Start()
    {
        fadeImage.color = Vector4.zero; // 초기화
    }

    public void RotateAnimation()
    {
        DOTween.KillAll();

        animShrimp.DORotate(new Vector3(0f, 0f, 360f), 0.6f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        animShrimp.DOMoveX(targetTrm.position.x, 6f).OnComplete(() => StartGame());
        RandomMoveY();
    }

    public void StartAnimation()
    {
        animShrimp.transform.DOMoveX(0, 3f).OnComplete(() => RotateAnimation());
        RandomMoveY();
    }

    public void RandomMoveY()
    {
        float randomY = Random.Range(-2f, 2f);
        animShrimp.DOMoveY(randomY, 0.7f).SetEase(Ease.InOutSine).OnComplete(() => RandomMoveY());
    }


    private void StartGame()
    {
        fadeImage.DOFade(1f,0.6f).OnComplete(() =>
        {
            GameManager.curStage = 1;
            GameManager.painGauge = 50;
            SceneManager.LoadScene("Stage1");
        });
    }

    // 나중에 필요하면 사용
    public void ExitGame()
    {
        Application.Quit();
    }
}
