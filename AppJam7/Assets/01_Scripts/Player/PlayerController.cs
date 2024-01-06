using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed;

    [Header("Boost")]
    [SerializeField] private float boostSpeed;
    [SerializeField] private KeyCode boostKey;
    private float curBoostSpeed;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Boost();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * (speed + curBoostSpeed);
        float y = Input.GetAxis("Vertical") * (speed + curBoostSpeed);

        Vector2 move = new Vector2(x, y);
        _rigid.velocity = move;
    }

    private void Boost()
    {
        if (Input.GetKey(boostKey))
        {
            curBoostSpeed = boostSpeed;
        }
        else
        {
            curBoostSpeed = 0;
        }
    }
}
