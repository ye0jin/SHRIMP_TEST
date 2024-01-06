using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed;
    private float lastXInput;

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

    [Header("Glass")]
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject glassPrefab;
    private bool hasGlass;

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

        Glass();
    }

    private void Glass()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1.5f, 1 << LayerMask.NameToLayer("Glass"));
        if (Input.GetKeyDown(KeyCode.Mouse1) && items.Length > 0 && !hasGlass)
        {
            hasGlass = true;
            Destroy(items[0].gameObject);
            glass.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && hasGlass)
        {
            hasGlass = false;
            glass.SetActive(false);
            GameObject g = Instantiate(glassPrefab, transform.position, Quaternion.identity);
            Vector2 dir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
            g.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 8, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        if (!IsDash)
        {
            float x = Input.GetAxisRaw("Horizontal") * speed;
            float y = Input.GetAxisRaw("Vertical") * speed;

            if (x != 0) lastXInput = x;
            sr.flipX = lastXInput > 0 ? true : false;

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

        if (collision.gameObject.CompareTag("Next"))
        {
            GameManager.Instance.NextStage();
        }
    }
}
