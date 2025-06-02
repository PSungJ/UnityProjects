using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkeletonDamage : MonoBehaviour
{
    private Rigidbody rb;
    private Animator ani;
    private NavMeshAgent navi;
    private readonly string playerTag = "Player";
    private readonly string bullet = "BULLET";
    private readonly int hashHit = Animator.StringToHash("IsHit");
    private readonly int hashDie = Animator.StringToHash("IsDie");
    private int hp;
    private int maxHp = 100;
    public bool isDie = false;
    [Header("UI")]
    public Image hpBar;
    public Text hpText;
    public Canvas canvas;
    public BoxCollider attackCol;

    void Start()
    {
        attackCol = transform.GetChild(4).GetComponent<BoxCollider>();
        canvas = GetComponentInChildren<Canvas>();
        hpBar.color = Color.green;
        hp = maxHp;
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.isKinematic = true;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.isKinematic = false;
        }
        else if (col.gameObject.CompareTag(bullet))
        {
            ani.SetTrigger(hashHit);
            Destroy(col.gameObject);
            hp -= 25;
            hp = Mathf.Clamp(hp, 0, maxHp);
            hpBar.fillAmount = (float)hp / maxHp;
            if (hpBar.fillAmount <= 0.3f)
                hpBar.color = Color.red;
            else if (hpBar.fillAmount <= 0.5f)
                hpBar.color = Color.yellow;

            hpText.text = $"HP : <color=#ff0000>{hp}</color>";
        }
        if (hp <= 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        ani.SetTrigger(hashDie);
        isDie = true;
        Destroy(gameObject, 10f);
        canvas.enabled = false;
    }
    private void EnableAttackCollider()
    {
        attackCol.enabled = true;
    }
    private void DisableAttackCollider()
    {
        attackCol.enabled = false;
    }
}
