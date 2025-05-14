using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Left Shift + W 동시 입력 시 총을 접는 애니메이션을 재생하는 스크립트
// 두 키중 하나라도 떼면 애니메이션이 멈추고 총을 겨누는 애니메이션이 재생되도록 구현
public class PlayerHandAnimation : MonoBehaviour
{
    public Animation ani;
    private readonly string running = "running";
    private readonly string runStop = "runStop";
    private readonly string fire = "fire";
    bool isRunning;
    void Start()
    {   // 자기자신의 자식 오브젝트를 찾고, 그 자식의 자식 오브젝트를 찾는다
        ani = transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        //ani = GetComponentsInChildren<Animation>()[0];
        isRunning = false;
    }

    void Update()
    {
        PlayerRun();
        PlayerFire();
    }

    public void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1") && !isRunning)
        {
            ani.Play(fire);
        }
    }

    private void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            ani.Play(running);
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ani.Play(runStop);
            isRunning = false;
        }
    }
}
