using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private float duration;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            rigid.velocity = Vector3.zero;
            yield return new WaitForSeconds(waitTime);

            Vector2 movePos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rigid.AddForce(movePos * speed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(duration);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GlassFragment"))
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            GameManager.Instance.TakeHeal(5);
            Destroy(gameObject);
        }
    }
}
