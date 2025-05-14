using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public bool isPlay = false;

    public void GameStart()
    {
        isPlay = true;
        SceneManager.LoadScene("PlayScene");    //����Ƽ�� ������ Scene���� ����
    }
    public void GameQuit()
    {
#if UNITY_EDITOR    //��ũ�� ���� : �����Ϸ��� �����Ϳ��� ���������� Ȯ��
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
