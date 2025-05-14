using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//F키 입력 시 Light ON, F키 재입력 시 Light OFF
//켜고 끌때 사운드 삽입
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
