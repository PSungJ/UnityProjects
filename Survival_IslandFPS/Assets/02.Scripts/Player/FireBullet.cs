using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//마우스 왼쪽    클릭으로 총알을 발사하는 스크립트
//뭐가 필요? 1.FirePos 발사위치  2.총알프리팹 3. 오디오소스 오디오클립
public class FireBullet : MonoBehaviour
{
    public Transform FirePos; //총알 발사 위치
    public GameObject BulletPrefab; //총알 프리팹
    public AudioClip FireSound; //총알 발사 사운드 클립
    private AudioSource Source; //오디오 소스 컴포넌트
    void Start()
    {
        Source = GetComponent<AudioSource>(); //오디오 소스 컴포넌트를 가져온다.
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼 클릭 시
        {
            Fire(); //총알 발사 함수 호출
        }

    }
    void Fire()
    {              //what    ,   where            , how rotaion
       Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
        //총알 프리팹을 발사 위치와 회전으로 인스턴스화
        Source.PlayOneShot(FireSound,1.0f); //총알 발사 사운드 재생
    }
}
