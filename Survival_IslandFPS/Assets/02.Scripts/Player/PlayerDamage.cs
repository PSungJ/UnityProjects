using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1. HP °ª
// 2. UI
// 3. Trigger Enter
public class PlayerDamage : MonoBehaviour
{
    private float hp;
    private float maxHp = 100f;
    public Image hpBar;
    public Text hpText;
    private readonly string attackTag = "PUNCH";

    void Start()
    {
        hp = maxHp;
        hpBar = GameObject.Find("Canvas-UI").transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = GameObject.Find("Canvas-UI").transform.GetChild(0).GetChild(1).GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(attackTag))
        {
            hp -= 10;
            hp = Mathf.Clamp(hp, 0, maxHp);
            hpBar.fillAmount = hp / maxHp;
            hpText.text = $"HP : <color=#ff0000>{hp}</color>";
            if (hpBar.fillAmount <= 0.3f)
                hpBar.color = Color.red;
            else if (hpBar.fillAmount <= 0.5f)
                hpBar.color = Color.yellow;

            if (hp <= 0)
            {
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        Debug.Log("YOU DIE");
    }
}
