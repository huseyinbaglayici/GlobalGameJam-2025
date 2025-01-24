using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float ROTSpeed;

    void FixedUpdate()
    {
        transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * ROTSpeed);
    }
}
