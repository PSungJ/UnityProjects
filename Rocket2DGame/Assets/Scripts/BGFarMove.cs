using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFarMove : MonoBehaviour
{
    public float speed;
    public Transform tr;
    public BoxCollider2D box2D;
    private float width;

    void Start()
    {
        tr = GetComponent<Transform>();
        box2D = GetComponent<BoxCollider2D>();
        speed = 5f;
        width = box2D.size.x; //BoxCollider�� x ������ ���� �ʺ�� ����
    }

    void Update()
    {
        //if (GameManager.instance.isGameOver == true)
        //{
        //    return; // ���⼭���� ���� �������� �������� ����
        //}

        if (tr.position.x <= -width*1.8f)
        {
            RePosition();
        }
        tr.Translate(Vector3.left * speed * Time.deltaTime);   
    }

    private void RePosition()
    {
        Vector2 offset = new Vector2(width * 2.8f, 0f);
        tr.position = (Vector2)tr.position + offset;
    }
}
