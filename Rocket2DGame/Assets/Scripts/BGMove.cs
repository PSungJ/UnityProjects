using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    //���� ���� : ���� �̵�����, �ӵ�, ����
    private float x = 0f, y = 0f;
    public float speed = 0.5f;

    public MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        BackGroundmove();
    }

    private void BackGroundmove()
    {
        x += Time.deltaTime * speed;
        meshRenderer.material.mainTextureOffset = new Vector2(x, y);
        //�޽��������� ���׸��� �ȿ� �����ؽ��� ������ = new ����2(x, y);
    }
}
