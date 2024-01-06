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

    [Header("")]
    [SerializeField] private Transform background;
    [SerializeField] private Transform fish;
    [SerializeField] private RectTransform title;

    [Header("애니메이션")]
    [SerializeField] private RectTransform targetTrm;
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
        fadeImage.DOFade(0f, 1f);
    }

    private void Start()
    {
        fadeImage.color = Vector4.one;
        RandomBack();
    }

    public void RandomBack()
    {
        float randomY = Random.Range(-0.4f, 0.6f);
        background.DOMoveY(randomY, 0.7f).SetEase(Ease.InOutSine);

        float randomY2 = Random.Range(-0.7f, 0.7f);
        fish.DOMoveY(randomY2, 0.7f).SetEase(Ease.InOutSine).OnComplete(() => {
            RandomBack();
        });
    }

    public void RotateAnimation()
    {
        animShrimp.DORotate(new Vector3(0f, 0f, 360f), 0.6f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        StartGame();
        background.GetComponent<Animator>().SetTrigger("Mouth");
    }

    public void StartAnimation()
    {
        DOTween.CompleteAll();

        StartCoroutine(TitleMove());

        fish.transform.DOMoveX(-13f, 2f);
        animShrimp.transform.DOMoveX(0, 3f).OnComplete(() => RotateAnimation());
        RandomMoveY();
    }

    private IEnumerator TitleMove()
    {
        while (title.anchoredPosition.x > 0)
        {
            title.Translate(Vector2.left * 10 * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    public void RandomMoveY()
    {
        float randomY = Random.Range(-2f, 2f);
        animShrimp.DOMoveY(randomY, 0.7f).SetEase(Ease.InOutSine).OnComplete(() => RandomMoveY());
    }


    private void StartGame()
    {
        fadeImage.DOFade(1f,2.2f).OnComplete(() =>
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
