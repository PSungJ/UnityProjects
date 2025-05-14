using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStop : MonoBehaviour
{
    public GameObject rainPrefab;
    public GameObject rainObj;
    void Start()
    {
        rainObj = Instantiate(rainPrefab);  //rainprefab을 인스턴스화 하여 rainObj에 저장
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
