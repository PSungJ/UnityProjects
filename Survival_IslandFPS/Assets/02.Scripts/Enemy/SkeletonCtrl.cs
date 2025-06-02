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
public class SkeletonCtrl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform tr;
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private SkeletonDamage damage;
    public float traceDist = 20.0f; //���� ����
    public float attackDist = 3.0f; //���� ����
    public Transform playerTr;
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    //�����Ҵ�� ���ÿ� ���ڿ��� �о Ž���� �ؼ� ã�� ���ڿ���  ������ ��ȯ
    void Start()
    {
        damage = GetComponent<SkeletonDamage>();
        tr = transform;
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>(); //�׺���̼� ������Ʈ ������
        playerTr = GameObject.FindWithTag("Player").transform;
        //���̶�Ű���� Player��� �±׸� ����������Ʈ�� Ʈ�������� ������
        //h =Input.GetAxis("Horizontal);
    }
    void Update()
    {
        if (damage == null) return;
        if (damage.isDie)
        {
            return;
        }

        float dist = Vector3.Distance(tr.position, playerTr.position);
        //float dist = (playerTr.position - tr.position).magnitude; //������ ũ��(�Ÿ�)�� �̿��ؼ� �Ÿ����
        if (dist <= attackDist) //�������϶�
        {
            PlayerAttack();
        }
        else if (dist <= traceDist) //�������϶�
        {
            PlayerTrace();

        }
        else //���ݵ� �ƴϰ� ������ �ƴҶ�
        {
            Idle();
        }



    }

    private void Idle()
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
        Vector3 attackTarget = (playerTr.position - tr.position).normalized;
        //����Ÿ�� - �ڽ� = ����    magnitude : ũ��  normalized : ����
        Quaternion rot = Quaternion.LookRotation(attackTarget);  // ����Ÿ���� �ٶ󺸰� ȸ��
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * 10f);     // �ε巴�� ȸ��
                                                                                                //Quaternion.Slerp : ��� �����Լ�    //�ڱ��ڽ�ȸ����, Ÿ��ȸ��, �ð���ŭ
    }
}
