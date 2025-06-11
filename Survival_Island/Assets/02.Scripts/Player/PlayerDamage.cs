using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//1.HP�� MAXHP��
//2IMAGE HPBAR TEXT
//3.OnTriggerEnter(Collider col) //�浹��
public class PlayerDamage : MonoBehaviour
{
    private float hp;
    private float maxHp = 100f; 
    public Image hpBar; 
    public Text hpText; 
    private readonly string punchTag = "PUNCH"; //���� ���� �±�  
    private readonly string swordTag = "SWORD"; //���� �� ���� �±�
    public GameObject BlindObj;
    void Start()
    {
        BlindObj = GameObject.Find("Canvas-UI").transform.GetChild(4).gameObject;
        hpBar = GameObject.Find("Canvas-UI").transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpBar.color = Color.green;
        hp = maxHp; //�ʱ� ü���� �ִ� ü������ ����
        //UI���� HPBar �̹��� ������Ʈ�� ������
        hpText = GameObject.Find("Canvas-UI").transform.GetChild(0).GetChild(1).GetComponent<Text>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(punchTag)) //���� ���ݿ� �¾��� ��
        {
            hp -= 5; //ü�� ����
            UIManager();

        }
        else if(other.CompareTag(swordTag))
        {
            hp -= 30; //ü�� ����
            UIManager();
        }
     
        if (hp <= 0) //ü���� 0 ���ϰ� �Ǹ�
        {
            PlayerDie(); //���� ó��
        }

    }

    private void UIManager()
    {
        hp = Mathf.Clamp(hp, 0, maxHp); //ü���� 0�� �ִ� ü�� ���̷� ����
        hpBar.fillAmount = hp / maxHp; //ü�¹� UI ������Ʈ
        hpText.text = $"HP :<color=#ff0000>{hp}</color>"; //ü�� �ؽ�Ʈ UI ������Ʈ
        if (hpBar.fillAmount <= 0.3f)
            hpBar.color = Color.red; //ü�¹ٰ� 30% ������ �� ���������� ����
        else if (hpBar.fillAmount <= 0.5f)
            hpBar.color = Color.yellow; //ü�¹ٰ� 50%�� �� ��������� ����
    }

    void PlayerDie()
    {
        BlindObj.SetActive(true);
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();   // ���� ������Ʈ�� ��� MonoBehaviour ��ũ��Ʈ ��������
        foreach (var script in scripts)
        {
            script.enabled = false;     // ��� ��ũ��Ʈ ��Ȱ��ȭ
        }
        Invoke("SceneMove", 3f);
    }
    void SceneMove()
    {
        SceneManager.LoadScene("EndScene");
    }
}
