using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; //�̱��� ���� : ��ü������ �ѹ��� �ǰ� �ٸ� Ŭ�������� ���� ����
    public GameObject asteroidPrefab; //asteroid prefab ����
    private float timePreve; //�ð� ����
    // �ּ�: �����ڰ� ���� �����ڰ� ����
    // Attribute�Ӽ� : �����ڰ� ���� ����Ƽ�� �о ����
    [Header("bool GameOver")]           //Attribute �Ӽ�
    public bool isGameOver = false;     //���ӿ��� ����
    [Header("CameraShake Logic Member")]
    public bool isShake = false;        //ī�޶� ��鸲 ����
    public Vector3 PosCamera;          //ī�޶� ��ġ ����
    public float beginTime;            //ī�޶� ��鸲 ���۽ð�
    [Header("HPBar UI")]
    public int hp;
    public int maxHP = 100;
    public Image hpBar;
    public Text hpText;
    [Header("GameOver UI")]
    public GameObject gameoverObj;
    public Text scoreTxt;
    private float curScore = 0; //���� ����
    private float plusScore = 1f;    //���� ������

    void Start()
    {
        if (instance == null)
            instance = this;    //this = GetComponent<GameManager>();
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject); // ���� �ٲ� �ı����� �ʴ´�.
        timePreve = Time.time;  //���� ���� �� ����ð��� ����
        hp = maxHP;
    }

     void Update()
    {       //����ð� - �����ð� = ������ �ð�
        if (Time.time - timePreve > 2.5f && !isGameOver)
        {
            AsteroidSpawn();
        }

        if (isShake == true)
        {
            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);
            Camera.main.transform.position += new Vector3(x, y, 0f);

            if (Time.time - beginTime > 0.3f)
            {
                isShake = false;    //ī�޶� ��鸲 ����
                Camera.main.transform.position = PosCamera; //ī�޶� ���� ��ġ�� ����
            }
        }
        ScoreCount();
    }

    private void ScoreCount()
    {
        curScore += plusScore * Time.realtimeSinceStartup; // ���� ����
        //Time.realtimeSinceStartup : ������ ������ ������ �ð��� �ʴ����� ��ȯ readonly�Ӽ�
        scoreTxt.text = $"{Mathf.FloorToInt(curScore)}"; //���� UI ����
                                                         //Mathf.FloorToInt() float������ �۰ų� ���� ������ ��ȯ
                                                         //Mathf.FloorToInt(3.7) �� ��� ���� 3�� ��ȯ
                                                         //Mathf.FloorToInt(-3.2) �� ��� ���� -4�� ��ȯ
    }

    public void TurnOn()
    {
        isShake = true; //ī�޶� ��鸲 ����
        PosCamera = Camera.main.transform.position; //ī�޶� ��鸮�� ���� ��ġ�� ����
        beginTime = Time.time;  //ī�޶� ��鸮�� ������ �ð� ����
    }
    private void AsteroidSpawn()
    {
        float randomY = Random.Range(-3.7f, 3.7f); //������ Y��ǥ ����
        Instantiate(asteroidPrefab, new Vector3(12f, randomY, asteroidPrefab.transform.position.z), Quaternion.identity);// ȸ������ �ʰ� ����
        //Instantiate : ������Ʈ ���� �Լ�(what?, where?, how ȸ�� �� ���ΰ�?)
        timePreve = Time.time; // ����ð� ����
    }
    public void RocketHealthPoint(int damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100); // ü���� 0~100���� ����
        hpBar.fillAmount = (float)hp / (float)maxHP;
        hpText.text = $"HP : <color=#ff0000>{hp}</color>";
        if (hp <= 0)
        {
            isGameOver = true;
            gameoverObj.SetActive(true);
            Invoke("LobbySceneMove", 3.0f);
            //��Ʈ�� ���ڸ� �о ���ϴ� �ð��� ȣ���ϴ� �Լ� - Invoke
        }
    }
    public void LobbySceneMove()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
