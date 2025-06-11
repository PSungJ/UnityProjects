using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class ZomBieDamage : MonoBehaviour //���X  ���۳�ƮX
{
    private Animator animator;
    private Rigidbody rb; //why?
    private NavMeshAgent agent;
    private readonly string playerTag = "Player"; 
    private readonly string jumpTag = "JUMPSURPORT"; 
    private readonly string bulletTag = "BULLET"; 
    private readonly int hashJump = Animator.StringToHash("IsJump"); 
    private readonly int hashHit = Animator.StringToHash("IsHit"); //�¾��� �� �ִϸ��̼� �ؽð�
    private readonly int hashDie = Animator.StringToHash("IsDie"); //�׾��� �� �ִϸ��̼� �ؽð�
    private int hp;
    private int maxHp = 100; //�ִ� ü��
    //���� �ִϸ��̼� �ؽð�
    bool isJumping = false; //���� ���θ� ��Ÿ���� ����
    public bool IsDie = false;
    [Header("UI")]
    public Image hpBar; //ü�¹� UI �̹���
    public Text hpText; //ü�� �ؽ�Ʈ UI
    public Canvas canvas; //ĵ���� UI
    public BoxCollider attackCol;
    void Start()
    {
        attackCol = transform.GetChild(19).GetComponent<BoxCollider>();
        //attackCol = GameObject.Find("AttackPoint").GetComponent<BoxCollider>(); //���� �ݶ��̴��� ã�´�.
        //���� �ݶ��̴� ������Ʈ�� �����´�.
        canvas = GetComponentInChildren<Canvas>(); //�ڽ� ������Ʈ���� ĵ���� ������Ʈ�� �����´�.
        hpBar.color = Color.green; 
        hp = maxHp; //�ʱ� ü���� �ִ� ü������ ����
        agent = GetComponent<NavMeshAgent>(); //�׺���̼� ������Ʈ ������Ʈ�� �����´�.
        animator = GetComponent<Animator>(); //�ִϸ����� ������Ʈ�� �����´�.
        rb = GetComponent<Rigidbody>(); //Rigidbody ������Ʈ�� �����´�.
    }
    private void OnCollisionEnter(Collision col) //�ݹ� �Լ�
    {
        if(col.gameObject.CompareTag(playerTag))
        {
            rb.isKinematic = true; //�浹�� Rigidbody�� ������ ȿ���� ����
            rb.mass = 1000f;
        }
        else if (col.gameObject.CompareTag(bulletTag)) 
        {
            animator.SetTrigger(hashHit); //�¾��� �� �ִϸ��̼� Ʈ���� ����
            Destroy(col.gameObject); //�Ѿ� ������Ʈ�� �ı�
            hp -= (int)col.gameObject.GetComponent<BulletCtrl>().zomBieDamage;
            hp = Mathf.Clamp(hp, 0, maxHp); //ü���� 0�� �ִ� ü�� ���̷� ����

            hpBar.fillAmount = (float)hp / maxHp; //ü�¹� UI ������Ʈ
            if(hpBar.fillAmount<= 0.3f)
                hpBar.color = Color.red; //ü�¹ٰ� 30% ������ �� ���������� ����
            else if(hpBar.fillAmount <= 0.5f)
                hpBar.color = Color.yellow; //ü�¹ٰ� 50%�� �� ��������� ����

            hpText.text =$"HP :<color=#ff0000>{hp}</color>"; //ü�� �ؽ�Ʈ UI ������Ʈ

        }
        if(hp <= 0) //ü���� 0 ���ϰ� �Ǹ�
        {
            Die();
        }
    }

    private void Die()
    {
        IsDie = true;
        GetComponent<CapsuleCollider>().enabled = false; //�ݶ��̴� ��Ȱ��ȭ
        GetComponent<Rigidbody>().isKinematic = true; //Rigidbody�� ������ ȿ���� ����
        animator.SetTrigger(hashDie); //�׾��� �� �ִϸ��̼� Ʈ���� ����
        Destroy(gameObject, 10f);
        canvas.enabled = false; //ĵ���� UI ��Ȱ��ȭ
        GameManager.Instance.UpdateKillCount(1);
    }

    private void OnCollisionExit(Collision col) //�ݹ� �Լ�
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.isKinematic = false; //�浹�� Rigidbody�� ������ ȿ���� �ش�.
            rb.mass = 70f; //���� �������� �ǵ�����.
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(jumpTag)&& isJumping==false) //�÷��̾�� �浹���� ��
        {
            isJumping = true;
            animator.SetTrigger(hashJump); //���� �ִϸ��̼� Ʈ���� ����
            agent.speed = 0.1f;
        }
    }
    void Update()
    {
        if(isJumping && agent.isOnOffMeshLink) //���� �ִϸ��̼��� ���� ���̰� �׺���̼� ������Ʈ�� �����޽� ��ũ�� ���� ��
        {
           StartCoroutine(EnemyJump()); //���� �ڷ�ƾ ����
        }


    }
    IEnumerator EnemyJump()
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0); //���� �ִϸ��̼� Ŭ�� ������ �����´�.
                                                                            //0 �⺻�ε��� ���̾� 
        yield return new WaitForSeconds(clipInfos.Length); // �ִϸ��̼� Ŭ���� ���̸�ŭ ����Ѵ�.
        isJumping = false; //���� ���¸� ����
        agent.speed = 3.5f; //���� �ӵ��� �ǵ�����.
    }
    public void EnableAttackCollider() //���� �ݶ��̴� Ȱ��ȭ/��Ȱ��ȭ
    {
        attackCol.enabled = true;
    }
    public void DisableAttackCollider()
    {
        attackCol.enabled = false;
    }
}
