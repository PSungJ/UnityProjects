using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// �̱��� ���� ���
// 1. ���� ������Ʈ : ��ü ������ �ѹ����ϰ�
// 2. ���� ���� : �ٸ� ��ũ��Ʈ���� ���� ������ �� �ֵ���
// 3. ���� ���� ���� : ������ ������ ������Ʈ�� �ı����� �ʵ��� �Ѵ�.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text scoreUI;
    public GameObject gameoverUI;
    public bool isGameOver = false;
    private int score = 0;

    void Awake()    //Start()���� ���� ����
    {
        if (instance == null)               //instance�� null�̶��(���� �������� ����)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)          //instance�� null�� �ƴϰ�, ���� ������Ʈ�� instance�� �ٸ��ٸ�
        {
            Debug.LogWarning("���� �ΰ� �̻��� ���ӸŴ����� �����մϴ�.");
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        scoreUI = GameObject.Find("Canvas-UI").GetComponentsInChildren<Text>()[0];
        gameoverUI = GameObject.Find("Canvas-UI").transform.GetChild(1).gameObject;
    }
    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0))  // ���ӿ��� + ���콺 ��Ŭ�� ����
        {
            // �� �����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            isGameOver = false;
        }
    }
    private void OnDestroy()
    {
        // �� �ε� �̺�Ʈ ���� (�޸� ���� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �ε�� ������ �ٽ� UI ã��
        var canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            scoreUI = canvas.transform.GetChild(0).GetComponent<Text>();
            gameoverUI = canvas.transform.GetChild(1).gameObject;
        }
    }
    public void AddScore(int newScore) // ���� �߰� �Լ�
    {
        if (!isGameOver)
        {
            score += newScore;
            scoreUI.text = $"Score : {score}";
        }
    }
    public void OnPlayerDie()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("����");
    }
}
