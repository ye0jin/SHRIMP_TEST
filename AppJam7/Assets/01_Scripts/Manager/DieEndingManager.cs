using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieEndingManager : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private Animator whaleAnim;
    [SerializeField] private Transform poopPos;
    [SerializeField] private GameObject poop;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        StartCoroutine(Co());
    }

    private IEnumerator Co()
    {
        fade.DOFade(0, 1);
        yield return new WaitForSeconds(1);

        whaleAnim.SetTrigger("Poop");
        yield return new WaitForSeconds(0.3f);
        GameObject g = Instantiate(poop, poopPos.position, Quaternion.identity);
        g.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);

        Destroy(whaleAnim.gameObject);

        yield return new WaitForSeconds(0.5f);

        while (text.color.a < 1)
        {
            text.fontSize += Time.deltaTime * 20;
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Menu");


        yield break;
    }
}
