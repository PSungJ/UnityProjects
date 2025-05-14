using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알 자기자신 스스로 Z축으로 빠르게 나아간다
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
