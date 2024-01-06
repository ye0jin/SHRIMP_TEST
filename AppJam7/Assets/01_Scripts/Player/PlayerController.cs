using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed;

    [Header("Boost")]
    public float CurBoostGauge;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float maxBoostGauge;
    [SerializeField] private float boostGaugeCharge;
    [SerializeField] private float boostGaugeUse;
    private float curBoostSpeed;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        CurBoostGauge = maxBoostGauge;
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
        if (Input.GetKey(KeyCode.Space) && CurBoostGauge > 0)
        {
            curBoostSpeed = boostSpeed;
            CurBoostGauge -= Time.deltaTime * boostGaugeUse;
        }
        else
        {
            curBoostSpeed = 0;
            if (CurBoostGauge < maxBoostGauge)
            {
                CurBoostGauge += Time.deltaTime * boostGaugeCharge;
            }
        }
    }
}
