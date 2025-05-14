using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStop : MonoBehaviour
{
    public GameObject rainPrefab;
    public GameObject rainObj;
    void Start()
    {
        rainObj = Instantiate(rainPrefab);  //rainprefab�� �ν��Ͻ�ȭ �Ͽ� rainObj�� ����
        //rainObj.transform.position = new Vector3(-250f, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(rainObj);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rainObj = Instantiate(rainPrefab);
        }
    }
}
