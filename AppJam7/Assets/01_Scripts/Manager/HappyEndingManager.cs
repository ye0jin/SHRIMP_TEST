using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HappyEndingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        StartCoroutine(Co());
    }

    private IEnumerator Co()
    {
        yield return new WaitForSeconds(2);

        while (text.color.a < 1)
        {
            text.fontSize += Time.deltaTime * 20;
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");

        yield break;
    }
}
