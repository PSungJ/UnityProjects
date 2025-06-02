using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//NavMeshAgent�� �̿� �ؼ�
//�÷��̾ ���� �����ȿ� ������ �����ϰ�
//���� �����ȿ� ������ �����ϴ� ���� ������ �ִϸ��̼� ����
//�������� ���ݹ����� ���Ϸ��� �Ÿ��� ���ؾ��� �÷��̾�� ������ ��ġ�� �˾ƾ���
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class ZomBieCtrl : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField] private Transform zomBieTr;
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private ZomBieDamage Z_damage;
    public float traceDist = 20.0f; //���� ����
    public float attackDist = 3.0f; //���� ����
    public Transform playerTr;
    public float rotSpeed = 30f;
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
       //�����Ҵ�� ���ÿ� ���ڿ��� �о Ž���� �ؼ� ã�� ���ڿ���  ������ ��ȯ
    void Start()
    {
        Z_damage = GetComponent<ZomBieDamage>();
        zomBieTr = transform;
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>(); //�׺���̼� ������Ʈ ������
        playerTr = GameObject.FindWithTag("Player").transform;
        //���̶�Ű���� Player��� �±׸� ����������Ʈ�� Ʈ�������� ������
       
    }
    void Update()
    {
        if (Z_damage == null) return;
        
        if (Z_damage.IsDie) //���� �׾��� ��
        {
            return; //������Ʈ ����
        }

        float dist = Vector3.Distance(zomBieTr.position,playerTr.position);
        //float dist =(playerTr.position - zomBieTr.position).magnitude; //������ ũ��(�Ÿ�)�� ����
        if (dist <= attackDist) //�������϶�
        {
            PlayerAttack();

        }
        else if(dist <= traceDist) //�������϶�
        {
            PlayerTrace();

        }
        else //���ݵ� �ƴϰ� ������ �ƴҶ�
        {
            PlayerIdle();
        }



    }

    private void PlayerIdle()
    {
        animator.SetBool(hashTrace, false);
        navi.isStopped = true; //���� ���� ���� �� ����
    }

    private void PlayerTrace()
    {
        animator.SetBool(hashAttack, false);
        animator.SetBool(hashTrace, true);
        navi.isStopped = false; //���� ���� �ȿ� ������ �׺� ���� ����
        navi.destination = playerTr.position; //�÷��̾��� ��ġ�� �������� ����
    }

    private void PlayerAttack()
    {
        animator.SetBool(hashAttack, true);

        navi.isStopped = true; //�������� �� �׺� ��������

        Vector3 attackTarget = (playerTr.position - zomBieTr.position).normalized; //���� ������ �Ÿ�
                                                                                   //Ÿ����ġ - �ڱ��ڽ� ���� ��ġ = ����
        Quaternion rot = Quaternion.LookRotation(attackTarget); //���� �÷��̾ �ٶ󺸰� ȸ��
        zomBieTr.rotation = Quaternion.Slerp(zomBieTr.rotation, rot, Time.deltaTime * rotSpeed); 
        //�ε巴�� ȸ��
        //��麸�� �Լ�  //�ڱ��ڽ�ȸ���� ,Ÿ��ȸ�� , �ð� ��ŭ ȸ�� 
    }
}
