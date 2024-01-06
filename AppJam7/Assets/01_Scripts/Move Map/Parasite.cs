using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    private bool isDown;

    private void Awake()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (transform.position.y < maxY && !isDown)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                if (transform.position.y >= maxY) isDown = true;
            }
            else if (transform.position.y > minY && isDown)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                if (transform.position.y <= minY) isDown = false;
            }
            yield return null;
        }
    }
}
