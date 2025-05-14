using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FŰ �Է� �� Light ON, FŰ ���Է� �� Light OFF
//�Ѱ� ���� ���� ����
public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private AudioSource source;
    public AudioClip lightOn;
    void Start()
    {
        flashlight = GetComponent<Light>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            source.PlayOneShot(lightOn, 1.0f);
        }
    }
}
