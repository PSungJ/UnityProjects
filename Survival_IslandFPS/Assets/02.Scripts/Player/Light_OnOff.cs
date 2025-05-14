using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player가 Collider에 닿았을 때 Light On
//Collider 밖으로 나가면 Light Off
public class Light_OnOff : MonoBehaviour
{
    public Light pointlight;
    private AudioSource source;
    public AudioClip lightOn;
    public AudioClip lightOff;
    void Start()
    {
        pointlight = GetComponent<Light>();
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pointlight.enabled = true;
            source.PlayOneShot(lightOn);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pointlight.enabled = false;
            source.PlayOneShot(lightOff);
        }
    }
}
