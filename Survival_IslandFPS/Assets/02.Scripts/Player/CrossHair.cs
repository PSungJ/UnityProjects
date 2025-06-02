using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ���� ���� �ð�, ���ӽð�, ũ�⺯ȭ
// ���� ����
public class CrossHair : MonoBehaviour
{
    private Transform tr;
    public Image crosshairImg;
    private float startTime = 0f;
    private float duration = 0.3f;  // 0.3�ʵ��� ũ�� ��ȭ
    private float minSize = 0.8f;
    private float maxSize = 1.2f;
    private Color originColor = Color.green;
    private Color gazeColor = Color.red;
    public bool isGaze = false;
    void Start()
    {
        crosshairImg = GetComponent<Image>();
        crosshairImg.color = originColor;
        tr = GetComponent<Transform>();
        startTime = Time.time;
        tr.localScale = Vector3.one * minSize;  // ���� ũ�� ����
                // x,y,z�� ��� ������ ũ���
    }

    void Update()
    {
        if (isGaze)
        {
            float t = (Time.time - startTime) / duration;
            tr.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, t);
            //Lerp : ���������Լ� - �ּҿ��� �ִ�� ������ �� �ε巴�� ����
            crosshairImg.color = gazeColor;
        }
        else
        {
            tr.localScale = Vector3.one * minSize;
            crosshairImg.color = originColor;
            startTime = Time.time;
        }
    }
}
