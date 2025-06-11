using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���콺 ����    Ŭ������ �Ѿ��� �߻��ϴ� ��ũ��Ʈ
//���� �ʿ�? 1.FirePos �߻���ġ  2.�Ѿ������� 3. ������ҽ� �����Ŭ��
public class FireBullet : MonoBehaviour
{
    public Transform FirePos; //�Ѿ� �߻� ��ġ
    public GameObject BulletPrefab; //�Ѿ� ������
    public AudioClip FireSound; //�Ѿ� �߻� ���� Ŭ��
    private AudioSource Source; //����� �ҽ� ������Ʈ
    public Animation ani;
    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridge;
    [Header("Reload")]
    public float reloadTime = 1.5f;
    public int maxBullet = 10;
    public int curBullet = 10;
    public bool isReloading = false;

    PlayerControll playerAni;
    void Start()
    {
        ani = this.transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        Source = GetComponent<AudioSource>();
        muzzleFlash = GetComponentsInChildren<ParticleSystem>()[0];
        cartridge = GetComponentsInChildren<ParticleSystem>()[1];
        playerAni = GetComponent<PlayerControll>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading && !playerAni.isRunning)
        {
            muzzleFlash.Play();
            cartridge.Play();
            Fire();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            muzzleFlash.Stop();
            cartridge.Stop();
        }

    }
    void Fire()
    {
        Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
        Source.PlayOneShot(FireSound,1.0f);

        isReloading = (--curBullet % maxBullet == 0);
        if (isReloading)
        {
            StartCoroutine(Reload());
            Source.Stop();
        }
    }
    IEnumerator Reload()
    {
        ani.Play("pump1");
        yield return new WaitForSeconds(reloadTime);
        curBullet = maxBullet;
        isReloading = false;
    }
}
