using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Transform tr;
    public CrossHair crosshair;
    void Start()
    {
        crosshair = GameObject.Find("Image-Aim").GetComponent<CrossHair>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        ray = new Ray(tr.position, tr.forward);
        //Debug.DrawRay(tr.position, tr.forward * 20f, Color.red);
        if(Physics.Raycast(ray, out hit, 20f, 1<<7 | 1<<8))
        {
            crosshair.isGaze = true;
            //Debug.Log($"Raycast Hit : {hit.collider.name}");
        }
        else
        {
            crosshair.isGaze = false;
        }
    }
}
