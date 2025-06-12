using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private CircleCollider2D circle2D;
    [SerializeField] private Animator ani;
    [SerializeField] private Rigidbody2D rig2D;
    private AudioClip deathClip;
    private AudioClip jumpClip;
    private float jumpForce = 500f;
    private int jumpCount = 0;
    private bool isDead = false;
    private bool isGrounded = false;
    private readonly string GroundTag = "isJump";
    private readonly string dieTag = "Die";
    void Start()
    {
        deathClip = Resources.Load("die") as AudioClip;
        jumpClip = Resources.Load("jump") as AudioClip;
        source = GetComponent<AudioSource>();
        circle2D = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        rig2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                AndroidInput();
                break;
            case RuntimePlatform.WindowsEditor:
                PcTest();
                break;
        }
        ani.SetBool(GroundTag, isGrounded);
    }

    private void AndroidInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);    // ���� ��ġ ��ġ������ ������
            if (touch.phase == TouchPhase.Began && jumpCount < 2)   // ��ġ ���ۼ��� && �ִ� ����Ƚ��
            {
                ani.SetTrigger("Jump");
                ani.SetBool(GroundTag, !isGrounded);
                jumpCount++;
                rig2D.velocity = Vector2.zero;
                rig2D.AddForce(new Vector2(0, jumpForce));
                source.PlayOneShot(jumpClip);
            }
            else if (touch.phase == TouchPhase.Ended && rig2D.velocity.y > 0)
            {
                rig2D.velocity = rig2D.velocity * 0.5f;
            }
        }
    }

    private void PcTest()
    {
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            ani.SetTrigger("Jump");
            ani.SetBool(GroundTag, !isGrounded);
            jumpCount++;
            rig2D.velocity = Vector2.zero;
            // ���������� �ӵ��� 0���� �ʱ�ȭ
            rig2D.AddForce(new Vector2(0, jumpForce));
            // �������� ���� ����
            source.PlayOneShot(jumpClip);
        }
        else if (Input.GetMouseButtonUp(0) && rig2D.velocity.y > 0)
        {
            //ani.SetBool(GroundTag, false);
            // ��Ŭ�� ��ư�� ���鼭 �ӵ��� y���� ������(=���� �ö󰡴� ���̶��)
            rig2D.velocity = rig2D.velocity * 0.5f;
            // �ӵ��� ������ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Dead") && !isDead)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].normal.y > 0.7)
        {                   /* ǥ���� �븻���� ������ ���� ���ϴ��� Ȯ��
                            ���� � ǥ���� ��ֺ��� y���� 0.7���� ũ�ٸ�(��, ���� ������ �����ٸ�)
                            �뷫 45���� ��縦 ����ä ǥ���� ���� ���Ѵٴ� �ǹ�
                            �̷��� �ϴ� ������ �÷��̾ ���鿡�� ������ �� ���ǿ� �� �굵�� �ϱ� ����*/
            isGrounded = true;
            jumpCount = 0;  // ���� ������ ����Ƚ�� �ʱ�ȭ
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        isGrounded= false;
    }
    private void Die()
    {
        isDead = true;
        ani.SetTrigger(dieTag);
        source.clip = deathClip;
        source.Play();
        rig2D.velocity = Vector2.zero;
        isDead = true;
        GameManager.instance.OnPlayerDie();
    }
}
