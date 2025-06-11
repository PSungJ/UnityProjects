using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private float hp;
    private float maxHp = 100f;
    public Image hpBar;
    public Text hpText;
    private readonly string swordTag = "SWORD";
    public GameObject BlindObj;
    void Start()
    {
        hp = maxHp;
        hpBar = GameObject.Find("Canvas-UI").transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpBar.color = Color.green;
        hpText = GameObject.Find("Canvas-UI").transform.GetChild(0).GetChild(1).GetComponent<Text>();
        BlindObj = GameObject.Find("Canvas-UI").transform.GetChild(2).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(swordTag))
        {
            hp -= 20;
            UIManager();
        }
        if (hp <= 0)
        {
            PlayerDie();
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
    private void PlayerDie()
    {
        BlindObj.SetActive(true);
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
        Invoke("SceneMove", 3f);
    }
    void SceneMove()
    {
        SceneManager.LoadScene("EndScene");
    }
}
