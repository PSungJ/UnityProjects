using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyEcast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Transform tr;
    CrossHair crossHair; // CrossHair ��ũ��Ʈ ����
    void Start()
    {
        crossHair = GameObject.Find("Image-Aim").GetComponent<CrossHair>(); // CrossHair ������Ʈ ã��
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        ray = new Ray(tr.position, tr.forward); // ���� ��ġ���� �� �������� ���� ����
        //Debug.DrawRay(tr.position, tr.forward * 20f, Color.red); // ���� �ð�ȭ
        if(Physics.Raycast(ray, out hit, 20f,1<<7 | 1<<8)) 
        {
           crossHair.isGaze = true; // CrossHair ��ũ��Ʈ�� isGaze�� true�� ����
        }
        else
        {
            crossHair.isGaze = false; // CrossHair ��ũ��Ʈ�� isGaze�� false�� ����
        }
    }
}
