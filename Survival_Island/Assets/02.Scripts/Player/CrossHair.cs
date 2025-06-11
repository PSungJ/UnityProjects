using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// �ܴ��� ������ �ð� , ���ӽð� ,ũ�� ��ȭ ���� ����
//���� ���� , �ܴ������� �ƴ��� �ľ�
public class CrossHair : MonoBehaviour
{
    Transform tr;
    public Image crosshairImg; // UI �̹��� ������Ʈ
    private float startTime = 0f;
    private float duration = 0.3f; // 0.3�� ���� ũ�� ��ȭ
    public float minSize = 0.7f; // �ּ� ũ��
    public float maxSize = 1.2f; // �ִ� ũ��
    private Color originColor = Color.white; // ���� ����
    private Color gazeColor = Color.red; // �ܴ��� ����
    public bool isGaze = false; // �ܴ��� ���� ����
    void Start()
    {
        crosshairImg = GetComponent<Image>(); // UI �̹��� ������Ʈ ��������
        crosshairImg.color = originColor; // ���� �������� �ʱ�ȭ
        tr = GetComponent<Transform>();
        startTime = Time.time; // ���� �ð� �ʱ�ȭ
        tr.localScale = Vector3.one * minSize; // �ʱ� ũ�� ����
                // x,y, z �� ��� ������ ũ��� ����
    }
    void Update()
    {
        if (isGaze)
        {
            float t = (Time.time - startTime) / duration; // �ð� ���� ���
            tr.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, t); // ũ�� ��ȭ
                                                                           //���������Լ�
            crosshairImg.color = gazeColor;
        }
        else
        {
            tr.localScale = Vector3.one * minSize; // �ּ� ũ��� ����
            crosshairImg.color = originColor; // ���� �������� ����
            startTime = Time.time; // ���� �ð� �ʱ�ȭ
        }
    }
}
