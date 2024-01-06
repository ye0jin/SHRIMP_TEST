using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HappyEndingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    private void Awake()
    {
        image.DOFade(0, 1f);
    }

    private void Start()
    {
        StartCoroutine(Co());
    }

    private IEnumerator Co()
    {
        SoundManager.Instance.PlayHappyEnding();
        yield return new WaitForSeconds(2);

        while (text.color.a < 1)
        {
            text.fontSize += Time.deltaTime * 20;
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        image.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene("Menu"));

        yield break;
    }
}
