using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class SkulCtrl : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private Animator ani;
    [SerializeField] private Transform skulTr;
    [SerializeField] private Transform playerTr;
    private readonly int attack = Animator.StringToHash("isAttack");
    private readonly int trace = Animator.StringToHash("isTrace");
    public float traceDistance = 15.0f;
    public float attackDistance = 3.0f;
    void Start()
    {
        navi = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        skulTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(skulTr.position, playerTr.position);
        if (distance <= attackDistance)
        {
            ani.SetBool(attack, true);
            navi.isStopped = true;
        }
        else if (distance <= traceDistance)
        {
            ani.SetBool(trace, true);
            ani.SetBool(attack, false);
            navi.isStopped = false;
            navi.destination = playerTr.position;
        }
        else
        {
            ani.SetBool(trace, false);
            navi.isStopped = true;
        }
    }
}
