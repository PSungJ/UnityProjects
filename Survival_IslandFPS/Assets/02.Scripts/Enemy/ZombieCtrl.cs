using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

// Player가 일정 범위내에 탐지되면 추적하고 공격범위안에 들어오면 공격(NavMeshAgent를 이용)
// 추적범위 공격범위를 구하려면 거리를 알아야하므로 Player와 Zomebie의 위치를 알아야한다.
public class ZombieCtrl : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform zombieTr;
    [SerializeField] private Transform playerTr;
    private readonly int attack = Animator.StringToHash("isAttack");
    private readonly int trace = Animator.StringToHash("isTrace");
    public float traceDistance = 15.0f; // 추적범위
    public float attackDistance = 3.0f; // 공격범위
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        navi = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombieTr = GetComponent<Transform>();
    }

    void Update()
    {
        float dist = Vector3.Distance(zombieTr.position, playerTr.position);    // 좀비위치와 플레이어 위치값 반환
        if (dist <= attackDistance)
        {
            animator.SetBool(attack, true);
            navi.isStopped = true;  //공격 중일 땐 추적 종료
        }
        else if(dist <= traceDistance)
        {
            animator.SetBool(attack, false);
            animator.SetBool(trace, true);
            navi.isStopped = false; //추적 범위안에 있을 땐 추적
            navi.destination = playerTr.position;   // 목적지를 플레이어 위치로 지정
        }
        else
        {
            animator.SetBool(trace, false);
            navi.isStopped = true;  //범위 밖에 있으면 추적하지 않음
        }
    }
}
