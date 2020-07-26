using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public GameObject TimeEffectObject;
    public GameObject TrailObject;
    private Vector3 sweepPosition = Vector3.zero;
    private bool isSweeped = true;

    public GameObject ImageObject;

    public GameObject SwordPrefab;

    public bool IsFlip = false;

    public int Hp = 10;
    public int TimeGauge = 100;

    public float MoveSpeed = 10.0f;
    public float DashSpeed = 10.0f;
    public float JumpPower = 10.0f;
    public float TimeDashPower = 10.0f;

    public LayerMask GroundLayer;

    public bool IsGround
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.25f, GroundLayer);
            if (hit.collider != null)
            {
                return true;
            }

            return false;
        }
    }

    private Rigidbody2D rigidbody;
    private SpriteRenderer sprRenderer;

    private Animator imageAnimator;

    [SerializeField]
    private bool isJump = false;

    [SerializeField]
    private bool isDash = false;

    [SerializeField]
    private int jumpCount = 0;

    private CameraController cameraCtrl;

    [SerializeField]
    private int dashTapCount;

    private Timer dashTapTimer;

    private Timer attackTimer;

    private Timer timeDashTimer;
    private Timer timeSweepTimer;

    private Timer gaugeTimer;

    private Timer timeDashGhostTimer;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();

        imageAnimator = ImageObject.GetComponent<Animator>();

        cameraCtrl = Camera.main.GetComponent<CameraController>();

        dashTapTimer = new Timer(0.25f, false) { IsEnable = true };
        attackTimer = new Timer(0.35f, false) { IsEnable = true };
        timeDashTimer = new Timer(0.5f, false) { IsEnable = true };
        timeSweepTimer = new Timer(5.0f, false) { IsEnable = true };
        gaugeTimer = new Timer(0.2f, true) { IsEnable = true };

        timeDashGhostTimer = new Timer(0.5f, false) { IsEnable = false };
    }

    private void Update()
    {
        if (IsGround)
            jumpCount = 0;

        Move();
        Jump();
        Attack();
        TimeDash();
        TimeSweep();

        if (TimeGauge < 100)
        {
            if (gaugeTimer.IsDone)
                TimeGauge += 1;
        }

        if (timeDashGhostTimer.IsDone)
        {
            GhostSprite[] ghostSprites = ImageObject.GetComponentsInChildren<GhostSprite>();
            for (int i = 0; i < ghostSprites.Length; i++)
                ghostSprites[i].IsEnable = false;
        }

        UIManager.Instance.SetHpGauge(Hp);
        UIManager.Instance.SetTimeGauge(TimeGauge);

        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.green);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Horizontal"))
        {
            if (!dashTapTimer.IsDone)
            {
                dashTapCount++;
                if (dashTapCount > 0)
                    isDash = true;
            }
            else
            {
                dashTapCount = 0;
                isDash = false;
            }

            dashTapTimer.IsEnable = true;
        }

        float speedTemp = (isDash) ? DashSpeed : MoveSpeed;

        Vector2 moveVec = Vector2.zero;

        if (Mathf.Abs(h) > 0.0f)
        {
            imageAnimator.SetBool("IsMoving", true);

            if (h < 0.0f)
            {
                IsFlip = true;
                ImageObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                moveVec = new Vector2(-speedTemp, 0.0f);
            }
            else if (h > 0.0f)
            {
                IsFlip = false;
                ImageObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                moveVec = new Vector2(speedTemp, 0.0f);
            }
        }
        else
        {
            imageAnimator.SetBool("IsMoving", false);
        }

        transform.Translate(moveVec * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (jumpCount < 1)
            {
                jumpCount++;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
                rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (attackTimer.IsDone)
            {
                attackTimer.IsEnable = true;

                Instantiate(SwordPrefab, transform.position, Quaternion.Euler(0, 0, (IsFlip) ? 180.0f : 0.0f));

                imageAnimator.SetTrigger("Attacking");
                cameraCtrl.Shake(0.1f, 10.0f);
            }
        }
    }

    private void TimeDash()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (TimeGauge > 10)
            {
                TimeGauge -= 10;

                timeDashGhostTimer.IsEnable = true;

                GhostSprite[] ghostSprites = ImageObject.GetComponentsInChildren<GhostSprite>();
                for (int i = 0; i < ghostSprites.Length; i++)
                    ghostSprites[i].IsEnable = true;

                TrailObject.SetActive(false);

                rigidbody.AddForce(((IsFlip) ? Vector2.left : Vector2.right) * TimeDashPower, ForceMode2D.Impulse);
            }
        }
    }

    private void TimeSweep()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (timeSweepTimer.IsDone)
            {
                if (isSweeped)
                {
                    if (TimeGauge > 50)
                    {
                        TimeGauge -= 50;

                        isSweeped = false;

                        sweepPosition = transform.position;

                        TrailObject.SetActive(true);

                        TimeEffectObject.SetActive(true);
                        TimeEffectObject.transform.position = sweepPosition;
                    }
                }
                else
                {
                    timeSweepTimer.IsEnable = true;

                    isSweeped = true;

                    timeDashGhostTimer.IsEnable = true;

                    GhostSprite[] ghostSprites = ImageObject.GetComponentsInChildren<GhostSprite>();
                    for (int i = 0; i < ghostSprites.Length; i++)
                        ghostSprites[i].IsEnable = true;

                    transform.position = sweepPosition;

                    TimeEffectObject.SetActive(false);
                }
            }
        }
    }

    private void Damaging()
    {
        cameraCtrl.Shake(0.5f, 10.0f);
        Hp--;
        if (Hp < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fist"))
        {
            Damaging();
        }

        if (collision.CompareTag("Dash"))
        {
            Damaging();
        }
    }
}