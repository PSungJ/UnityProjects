using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 조준 시작 시간, 지속시간, 크기변화
// 색상 변경
public class CrossHair : MonoBehaviour
{
    private Transform tr;
    public Image crosshairImg;
    private float startTime = 0f;
    private float duration = 0.3f;  // 0.3초동안 크기 변화
    private float minSize = 0.8f;
    private float maxSize = 1.2f;
    private Color originColor = Color.green;
    private Color gazeColor = Color.red;
    public bool isGaze = false;
    void Start()
    {
        crosshairImg = GetComponent<Image>();
        crosshairImg.color = originColor;
        tr = GetComponent<Transform>();
        startTime = Time.time;
        tr.localScale = Vector3.one * minSize;  // 최초 크기 설정
                // x,y,z축 모두 동일한 크기로
    }

    void Update()
    {
        if (isGaze)
        {
            float t = (Time.time - startTime) / duration;
            tr.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, t);
            //Lerp : 선형보간함수 - 최소에서 최대로 조정될 때 부드럽게 변형
            crosshairImg.color = gazeColor;
        }
        else
        {
            tr.localScale = Vector3.one * minSize;
            crosshairImg.color = originColor;
            startTime = Time.time;
        }
    }
}
