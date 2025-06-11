using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ����Ʈ Ű�� wŰ�� ���ÿ� ������ ��  ���� ���� �ִϸ��̼��� ����ϴ� ��ũ��Ʈ ����
//���� ����Ʈ Ű�� wŰ ���߿� �ϳ��� ���� �ִϸ��̼��� ���߰� ���� �ܴ��� �ִϸ��̼��� ����ǵ��� ����
public class PlayerHandAnimation : MonoBehaviour
{
    public Animation anim;
    private readonly string runAni = "running";
    private readonly string runStopAni = "runStop";
    private readonly string FireAni = "fire";
    private readonly string FireInput = "Fire1";
    public bool isRunning;
    FireBullet fireBullet;
    void Start()
    {    
        //�ڱ��ڽ��� ù��° �ڽ� ������Ʈ�� ã�� �� �ڽ���  ������Ʈ�� ù��° �ڽ� ������Ʈ�� ã�´�
        anim = transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        isRunning = false;
        fireBullet = GetComponent<FireBullet>();
    }


    void Update()
    {
        PlayerRunAni();
        PlayerFire();

    }

    public void PlayerFire()
    {
        if (fireBullet.isReloading) return;

        if (Input.GetButtonDown(FireInput) && !isRunning)
        {
            anim.Play(FireAni);
        }
    }

    private void PlayerRunAni()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.Play(runAni);
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.Play(runStopAni);
            isRunning = false;
        }
    }
}
