using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Left Shift + W ���� �Է� �� ���� ���� �ִϸ��̼��� ����ϴ� ��ũ��Ʈ
// �� Ű�� �ϳ��� ���� �ִϸ��̼��� ���߰� ���� �ܴ��� �ִϸ��̼��� ����ǵ��� ����
public class PlayerHandAnimation : MonoBehaviour
{
    public Animation ani;
    private readonly string running = "running";
    private readonly string runStop = "runStop";
    private readonly string fire = "fire";
    bool isRunning;
    void Start()
    {   // �ڱ��ڽ��� �ڽ� ������Ʈ�� ã��, �� �ڽ��� �ڽ� ������Ʈ�� ã�´�
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
