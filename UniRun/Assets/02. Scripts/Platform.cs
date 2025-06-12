using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles = new GameObject[3];
    private bool isStep;    // �÷��̾ ������ ��Ҵ��� ���θ� ����

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ�� �� ȣ��, Ȱ��ȭ ��Ȱ��ȭ�� �ݺ��� �� ���� ȣ��
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i] = transform.GetChild(i).gameObject;
        }
        isStep = false;
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);   // 0~2������ �������� 0�϶��� Ȱ��ȭ
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
        Debug.Log("Platform Ȱ��ȭ");
    }
    private void Start()
    {
        GameManager.instance.isGameOver = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isStep)
        {
            isStep = true;
            GameManager.instance.AddScore(1);   // ���� �߰�
        }
        Debug.Log("�浹");
    }
    private void OnDisable()
    {
        Debug.Log("Platform ��Ȱ��ȭ");
    }
}
