using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    enum CheckType
    {
        Heal,
        Pain
    }

    [SerializeField] private GameObject plasticEffect;
    [SerializeField] private CheckType checkType;
    private ObstacleParent parent;

    private void Awake()
    {
        parent = GetComponentInParent<ObstacleParent>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Instantiate(plasticEffect, transform.position, Quaternion.identity);

            if (checkType == CheckType.Heal && player.IsDash)
            {
                parent.Heal();
            }
            else if (checkType == CheckType.Pain && player.IsDash)
            {
                parent.Pain();
            }
        }
    }
}
