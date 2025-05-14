using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float h = 0f;   //���� �̵� A,D
    private float v = 0f;   //���� �̵� W,S
    public float speed = 5f;
    private Transform tr;
    public GameObject effect;
    public AudioSource source;  //����� �ҽ�
    public AudioClip hitclip;   //����� Ŭ��
    private Vector2 StartPos = Vector2.zero;    //������ġ
    public Transform firePos;   //�Ѿ� �߻� ��ġ
    public GameObject coinPrefab;   //�Ѿ�
    void Start()
    {
        source = GetComponent<AudioSource>();   //����� �ҽ� ���۳�Ʈ �ҷ�����
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if (GameManager.instance.isGameOver == true)
        {
            return; // ���⼭���� ���� �������� �������� ����
        }
        if (Application.platform == RuntimePlatform.WindowsEditor) //�����쿡�� ���� ��
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            Vector3 Normal = (h * Vector3.right) + (v * Vector3.up);
            tr.Translate(Normal.normalized * speed * Time.deltaTime);
            //tr.Translate(Vector3.right * h * speed * Time.deltaTime);
            //tr.Translate(Vector3.up * v * speed * Time.deltaTime);
        }
        if (Application.platform == RuntimePlatform.Android) //�ȵ���̵忡�� ���� ��
        {
            if (Input.touchCount > 0) //�� ���̶� ��ġ�� �Ǿ��ٸ�
            {
                Touch touch = Input.GetTouch(0);
                            //GetTouch(0) : ��ġ�� ��ġ���� �迭�� �����ϰ� ù��° ��ġ�� ��ġ�� ������
                switch (touch.phase)
                {
                    case TouchPhase.Began:  //��ġ ����
                        StartPos = transform.position;  //��ġ�� ��ġ ����
                        break;
                    case TouchPhase.Moved:  //��ġ�� �̵�
                        Vector3 touchDelta = touch.position - StartPos; //��ġ�� ��ġ - ������ġ �Ÿ��� ����
                        Vector3 moveDir = new Vector3(touchDelta.x, touchDelta.y, 0f);
                        tr.Translate(moveDir.normalized * speed * Time.deltaTime);
                        StartPos = touch.position;
                        break;
                        
                }
            }
        }


            #region �ʺ��� ����
            //if (tr.position.x >= 7.6f)
            //    tr.position = new Vector3(7.6f, tr.position.y, tr.position.z);
            //else if (tr.position.x <= -7.8f)
            //    tr.position = new Vector3(-7.8f, tr.position.y, -tr.position.z);

            //if (tr.position.y >= 4.3f)
            //    tr.position = new Vector3(tr.position.x, 4.3f, tr.position.z);
            //else if (tr.position.y <= -4.3)
            //    tr.position = new Vector3(tr.position.x, -4.3f, -tr.position.z);
            #endregion

            #region �߱��� ����
            tr.position = new Vector3(Mathf.Clamp(tr.position.x, -7f, 7f), Mathf.Clamp(tr.position.y, -4.3f, 4.3f), tr.position.z);
                                //Mathf.Clamp : ���� �����ϴ� �Լ� (������, min, max)
        #endregion
    }
    //�浹 Ʈ���� �ݹ� �Լ�
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Asteroid")
        {
            Destroy(col.gameObject); //�浹�� ������Ʈ ���� (�)
            Debug.Log($"�浹");
            var eff = Instantiate(effect, col.transform.position, Quaternion.identity); //����Ʈ ����
            Destroy(eff ,1f); //����Ʈ ����
            GameManager.instance.TurnOn();  // GameManger�� ī�޶� ��鸲 �Լ� ȣ��
            source.PlayOneShot(hitclip, 1f);// �Ҹ��� �ѹ��� ���
            //PlayOneShot:�Ҹ��� �ѹ��� ���(������, ����ũ��(�ִ�1f))
            GameManager.instance.RocketHealthPoint(50);
        }
    }
    public void Fire()
    {
        //������ ���� �Լ�(What?, Where?, How Rotation)
        Instantiate(coinPrefab, firePos.position, Quaternion.identity); //�Ѿ� ����
        Destroy(coinPrefab, 1f);    //1�� �� �Ѿ� ����
    }
}
