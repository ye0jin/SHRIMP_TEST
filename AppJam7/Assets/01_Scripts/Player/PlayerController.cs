using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed;

    [Header("Boost")]
    public float CurDashGauge;
    public bool IsDash;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float maxDashGauge;
    [SerializeField] private float DashCharge;
    [SerializeField] private float DashUse;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        CurDashGauge = maxDashGauge;
    }

    private void Update()
    {
        Move();
        Dash();
    }

    private void Move()
    {
        if (!IsDash)
        {
            float x = Input.GetAxis("Horizontal") * speed;
            float y = Input.GetAxis("Vertical") * speed;

            Vector2 move = new Vector2(x, y);
            _rigid.velocity = move;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CurDashGauge - DashUse > 0 && !IsDash)
        {
            IsDash = true;
            CurDashGauge -= DashUse;
            StartCoroutine(DashCoroutine());
        }

        if (CurDashGauge < maxDashGauge && !IsDash)
        {
            CurDashGauge += Time.deltaTime * DashUse;
        }
    }

    private IEnumerator DashCoroutine()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(x, y);

        _rigid.velocity = Vector2.zero;
        _rigid.AddForce(move * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        IsDash = false;
        yield break;
    }
}
