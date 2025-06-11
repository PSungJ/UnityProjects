using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//���콺 ����    Ŭ������ �Ѿ��� �߻��ϴ� ��ũ��Ʈ
//���� �ʿ�? 1.FirePos �߻���ġ  2.�Ѿ������� 3. ������ҽ� �����Ŭ��
public class FireBullet : MonoBehaviour
{
    public Transform FirePos; //�Ѿ� �߻� ��ġ
    public GameObject BulletPrefab; //�Ѿ� ������
    public AudioClip FireSound; //�Ѿ� �߻� ���� Ŭ��
    private AudioSource Source; //����� �ҽ� ������Ʈ
    public ParticleSystem muzzleFlash;  // �ѱ� ����Ʈ
    public ParticleSystem cartridge;

    [Header ("Reload")]
    public readonly float reloadTime = 1.4f;    // ������ �ð�
    public readonly int maxBullet = 10;         // �ִ� �Ѿ� ��
    public int currentBullet = 10;              // ���� �Ѿ� ��
    public bool isReloading = false;            // ������ ����
    public Animation ani;
    WeaponChange weaponChange;
    PlayerHandAnimation playerHandAnimation;
    private float timePrev;
    void Start()
    {
        playerHandAnimation = GetComponent<PlayerHandAnimation>();
        weaponChange = GetComponent<WeaponChange>();
        Source = GetComponent<AudioSource>(); //����� �ҽ� ������Ʈ�� �����´�.
        ani = this.transform.GetChild(0).GetChild(0).GetComponent<Animation>();
        muzzleFlash = GetComponentsInChildren<ParticleSystem>()[0];
        cartridge = GetComponentsInChildren<ParticleSystem>()[1];
        timePrev = Time.time;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && weaponChange.isHaveAR && playerHandAnimation.isRunning == false && !isReloading)
        {
            if (Time.time - timePrev > 0.1f)
            {
                Fire();
                timePrev = Time.time;
            }
        }
        if (Input.GetMouseButtonDown(0) && !isReloading && playerHandAnimation.isRunning == false) //���콺 ���� ��ư Ŭ�� ��
        {
            muzzleFlash.Play();
            cartridge.Play();
            Fire(); //�Ѿ� �߻� �Լ� ȣ��
        }
        else if (Input.GetMouseButtonUp(0))
        {
            muzzleFlash.Stop();
            cartridge.Stop();
        }
    }
    void Fire()
    {              //what    ,   where            , how rotaion
        Instantiate(BulletPrefab, FirePos.position, FirePos.rotation);
        //�Ѿ� �������� �߻� ��ġ�� ȸ������ �ν��Ͻ�ȭ
        Source.PlayOneShot(FireSound,1.0f); //�Ѿ� �߻� ���� ���
        isReloading = (--currentBullet % maxBullet == 0);
        if (isReloading)
        {
            StartCoroutine(Reload());
            Source.Stop();
        }
    }
    IEnumerator Reload()
    {
        ani.Play("pump2");
        yield return new WaitForSeconds(reloadTime);
        currentBullet = maxBullet;
        isReloading = false;
    }
}
