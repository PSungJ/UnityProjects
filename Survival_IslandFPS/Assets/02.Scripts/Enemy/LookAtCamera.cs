using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform CanvarTr;
    public Transform CameraTr;
    void Start()
    {
        CanvarTr = this.transform;
        CameraTr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CanvarTr.LookAt(CameraTr);
    }
}
