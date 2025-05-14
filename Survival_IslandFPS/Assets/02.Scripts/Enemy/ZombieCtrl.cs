using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

// Player�� ���� �������� Ž���Ǹ� �����ϰ� ���ݹ����ȿ� ������ ����(NavMeshAgent�� �̿�)
// �������� ���ݹ����� ���Ϸ��� �Ÿ��� �˾ƾ��ϹǷ� Player�� Zomebie�� ��ġ�� �˾ƾ��Ѵ�.
public class ZombieCtrl : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform zombieTr;
    [SerializeField] private Transform playerTr;
    private readonly int attack = Animator.StringToHash("isAttack");
    private readonly int trace = Animator.StringToHash("isTrace");
    public float traceDistance = 15.0f; // ��������
    public float attackDistance = 3.0f; // ���ݹ���
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        navi = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieTr = GetComponent<Transform>();
    }

    void Update()
    {
        float dist = Vector3.Distance(zombieTr.position, playerTr.position);    // ������ġ�� �÷��̾� ��ġ�� ��ȯ
        if (dist <= attackDistance)
        {
            animator.SetBool(attack, true);
            navi.isStopped = true;  //���� ���� �� ���� ����
        }
        else if(dist <= traceDistance)
        {
            animator.SetBool(attack, false);
            animator.SetBool(trace, true);
            navi.isStopped = false; //���� �����ȿ� ���� �� ����
            navi.destination = playerTr.position;   // �������� �÷��̾� ��ġ�� ����
        }
        else
        {
            animator.SetBool(trace, false);
            navi.isStopped = true;  //���� �ۿ� ������ �������� ����
        }
    }
}
