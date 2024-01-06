using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private GameObject glassFragment;
    [SerializeField] private int count;
    [SerializeField] private float rand;

    private void Destroy()
    {
        for (int i=0; i<count; i++)
        {
            Vector2 pos = (Vector2)transform.position + new Vector2(Random.Range(-rand, rand), Random.Range(-rand, rand));
            Instantiate(glassFragment, pos, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player.IsDash)
            {
                Destroy();
            }
        }
    }
}
