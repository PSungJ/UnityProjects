using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѿ� �ڱ��ڽ� ������ Z������ ������ ���ư���
public class BulletCtrl : MonoBehaviour
{
    public float speed = 1500f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
    }
}
