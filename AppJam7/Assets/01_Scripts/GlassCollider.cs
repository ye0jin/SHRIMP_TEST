using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCollider : MonoBehaviour
{
    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController p))
        {
            print("dd");
            p.AddGlass(transform.parent.gameObject);
        }
    }
}
