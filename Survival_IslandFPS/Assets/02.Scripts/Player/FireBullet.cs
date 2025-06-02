using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���콺 ����    Ŭ������ �Ѿ��� �߻��ϴ� ��ũ��Ʈ
//���� �ʿ�? 1.FirePos �߻���ġ  2.�Ѿ������� 3. ������ҽ� �����Ŭ��
public class FireBullet : MonoBehaviour
{
    public Transform FirePos; //�Ѿ� �߻� ��ġ
    public GameObject BulletPrefab; //�Ѿ� ������
    public AudioClip FireSound; //�Ѿ� �߻� ���� Ŭ��
    private AudioSource Source; //����� �ҽ� ������Ʈ
    void Start()
    {
        Source = GetComponent<AudioSource>(); //����� �ҽ� ������Ʈ�� �����´�.
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư Ŭ�� ��
        {
            Fire(); //�Ѿ� �߻� �Լ� ȣ��
        }

    }
    void Fire()
    {              //what    ,   where            , how rotaion
       Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
        //�Ѿ� �������� �߻� ��ġ�� ȸ������ �ν��Ͻ�ȭ
        Source.PlayOneShot(FireSound,1.0f); //�Ѿ� �߻� ���� ���
    }
}
