using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGNearMove : MonoBehaviour
{
    public float speed;
    public Transform tr;
    public BoxCollider2D box2D;
    private float width;

    void Start()
    {
        tr = GetComponent<Transform>();
        box2D = GetComponent<BoxCollider2D>();
        speed = 10f;
        width = box2D.size.x;
    }

     void Update()
    {
        //if (GameManager.instance.isGameOver == true)
        //{
        //    return; // ���⼭���� ���� �������� �������� ����
        //}

        if (tr.position.x <= -width * 1.8f)
        {
            Vector2 offset = new Vector2(width * 2.98f, 0f);
            tr.position = (Vector2)tr.position + offset;
        }
        tr.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
