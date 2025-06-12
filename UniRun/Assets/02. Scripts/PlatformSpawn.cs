using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platformPrefab;   // ���� ������
    public int count = 3;   // ���� ��������
    public float timeBetSpawnMin = 1.25f;   // ���� ��ġ �ð� ���� �ּҰ�
    public float timeBetSpawnMax = 2.25f;   // ���� ��ġ �ð� ���� �ִ밪
    private float timeBetSpawn; // ���� ��ġ �ð����� ����

    public float yMin = -3.5f;  // ���� ���� y��ǥ ��ġ �ּҰ�
    public float yMax = 1.5f;   // ���� ���� y��ǥ ��ġ �ִ밪
    private float xPos = 20f;   // ������ ������ x��ǥ
    
    private GameObject[] platforms; //�̸� ������ ���ǵ�
    private int currentIndex = 0;   // ���� Ȱ��ȭ�� ������ �ε���
    private Vector2 poolPosition = new Vector2(0f, -25f);   // Ǯ ��ġ
    private float lastSpawnTime;    //���������� ������ ������ �ð�

    void Start()
    {
        platformPrefab = Resources.Load("Platform") as GameObject;
        platforms = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }
        lastSpawnTime = 0f; // ������ ��ġ ���� �ʱ�ȭ
        timeBetSpawn = 0f;  // ������ ��ġ������ �ð� ������ 0���� �ʱ�ȭ
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);  // ��ġ �ð� ����
            float yPos = Random.Range(yMin, yMax);  // ��ġ ��ġ ����
            platforms[currentIndex].SetActive(false);   // ������Ʈ ���� OnDisable ȣ��
            platforms[currentIndex].SetActive(true);  // �ٽ� Ű�� OnEnable ȣ��

            platforms[currentIndex].transform.position = new Vector3(xPos, yPos);
            currentIndex++;

            if (currentIndex >= count)
                currentIndex = 0;
        }
    }
}
