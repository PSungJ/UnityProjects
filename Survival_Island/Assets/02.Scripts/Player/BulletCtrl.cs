using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�ڱ��ڽ� ������ z������ ���� �̵� �Ѵ�. 
public class BulletCtrl : MonoBehaviour
{
    public float Speed = 1500f;
    private Rigidbody rb;
    public float zomBieDamage = 20f;
    public float skeletonDamage = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed);
        Destroy(this.gameObject, 3f); //3�� �Ŀ� �Ѿ� ������Ʈ�� �ı�
    }
    
}
