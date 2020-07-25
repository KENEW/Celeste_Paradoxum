//	Copyright (c) KimPuppy.
//	http://bakak112.tistory.com/

using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    public float DashSpeed = 10.0f;
    public float JumpPower = 10.0f;

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

    [SerializeField]
    private int attackTapCount;

    private Timer attackTapTimer;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();

        cameraCtrl = Camera.main.GetComponent<CameraController>();

        dashTapTimer = new Timer(0.25f, false) { IsEnable = true };
        attackTapTimer = new Timer(0.75f, false) { IsEnable = true };
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
            isJump = true;
    }

    private void Update()
    {
        Move();
        Jump();
        Attack();

        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.green);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("Down");
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

        Vector2 moveVec = Vector2.zero;

        if (Mathf.Abs(h) > 0.0f)
        {
            //animator.SetBool("IsWalking", true);

            if (h < 0.0f)
            {
                sprRenderer.flipX = true;
                moveVec = new Vector2(-MoveSpeed, 0.0f);
            }
            else if (h > 0.0f)
            {
                sprRenderer.flipX = false;
                moveVec = new Vector2(MoveSpeed, 0.0f);
            }
        }
        else
        {
            //animator.SetBool("IsWalking", false);
        }

        //animator.SetBool("IsDashing", isDash);

        moveVec = (isDash) ? moveVec * 2.0f : moveVec;

        transform.Translate(moveVec * Time.deltaTime);
    }

    private void Jump()
    {
        if (!isJump) return;
        isJump = false;

        if (IsGround) jumpCount = 0;
        if (jumpCount < 2)
        {
            jumpCount++;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(Vector2.up * JumpPower * (1.0f + ((jumpCount - 1) * 0.35f)), ForceMode2D.Impulse);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraCtrl.Shake(0.25f, 5.0f, true, true, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraCtrl.Shake(0.25f, 5.0f, true, false, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraCtrl.Shake(0.25f, 5.0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cameraCtrl.Scaling(2.0f);
            cameraCtrl.Rotating(30.0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cameraCtrl.SetScale(2.0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            cameraCtrl.SetScale(1.0f);
        }

        if (Input.GetButtonDown("Attack"))
        {
            if (!attackTapTimer.IsDone)
            {
                attackTapCount++;
            }
            else
            {
                attackTapCount = 0;
            }
            attackTapTimer.IsEnable = true;

            //animator.SetTrigger("Attacking");
            //animator.SetInteger("AttackCount", attackTapCount);

            cameraCtrl.Shake(0.75f, 5.0f, true);
        }
    }
}