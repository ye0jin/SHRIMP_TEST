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

    [Header("Camera")]
    [SerializeField] private float cameraMoveSpeed;
    private Transform cam;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sr;
 
    private void Awake()
    {
        cam = Camera.main.transform;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        CurDashGauge = maxDashGauge;
    }

    private void Update()
    {
        Move();
        Dash();
        CameraMove();
    }

    private void Move()
    {
        if (!IsDash)
        {
            float x = Input.GetAxisRaw("Horizontal") * speed;
            float y = Input.GetAxisRaw("Vertical") * speed;

            sr.flipX = x > 0 ? true : false;

            Vector2 move = new Vector2(x, y);
            rigid.velocity = move;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CurDashGauge - DashUse > 0 && !IsDash)
        {
            IsDash = true;
            CurDashGauge -= DashUse;
            anim.SetTrigger("Dash");
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

        rigid.velocity = Vector2.zero;
        rigid.AddForce(move * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        IsDash = false;
        yield break;
    }

    private void CameraMove()
    {
        Vector3 pos = new Vector3(Vector2.Lerp(cam.transform.position, transform.position, Time.deltaTime * cameraMoveSpeed).x, Vector2.Lerp(cam.transform.position, transform.position, Time.deltaTime * cameraMoveSpeed).y, cam.position.z);
        cam.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Parasite"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
