using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//NavMeshAgent를 이용 해서
//플레이어가 추적 범위안에 들어오면 추적하고
//공격 범위안에 들어오면 공격하는 로직 구현과 애니메이션 연동
//추적범위 공격범위를 구하려면 거리를 구해야함 플레이어랑 좀비의 위치를 알아야함
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class SkeletonCtrl : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform tr;
    [SerializeField] private NavMeshAgent navi;
    [SerializeField] private SkeletonDamage damage;
    public float traceDist = 20.0f; //추적 범위
    public float attackDist = 3.0f; //공격 범위
    public Transform playerTr;
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    //동적할당과 동시에 문자열을 읽어서 탐색을 해서 찾은 문자열을  정수로 변환
    void Start()
    {
        damage = GetComponent<SkeletonDamage>();
        tr = transform;
        animator = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>(); //네비게이션 컴포넌트 가져옴
        playerTr = GameObject.FindWithTag("Player").transform;
        //하이라키에서 Player라는 태그를 가진오브젝트의 트랜스폼을 가져옴
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
        //float dist = (playerTr.position - tr.position).magnitude; //벡터의 크기(거리)를 이용해서 거리계산
        if (dist <= attackDist) //공격중일때
        {
            PlayerAttack();
        }
        else if (dist <= traceDist) //추적중일때
        {
            PlayerTrace();

        }
        else //공격도 아니고 추적도 아닐때
        {
            Idle();
        }



    }

    private void Idle()
    {
        animator.SetBool(hashTrace, false);
        navi.isStopped = true; //추적 범위 밖일 때 정지
    }

    private void PlayerTrace()
    {
        animator.SetBool(hashAttack, false);
        animator.SetBool(hashTrace, true);
        navi.isStopped = false; //추적 범위 안에 들어오면 네비 추적 시작
        navi.destination = playerTr.position; //플레이어의 위치를 목적지로 설정
    }

    private void PlayerAttack()
    {
        animator.SetBool(hashAttack, true);
        navi.isStopped = true; //공격중일 때 네비 추적정지
        Vector3 attackTarget = (playerTr.position - tr.position).normalized;
        //공격타겟 - 자신 = 방향    magnitude : 크기  normalized : 방향
        Quaternion rot = Quaternion.LookRotation(attackTarget);  // 공격타겟을 바라보게 회전
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * 10f);     // 부드럽게 회전
                                                                                                //Quaternion.Slerp : 곡면 보간함수    //자기자신회전값, 타겟회전, 시간만큼
    }
}
