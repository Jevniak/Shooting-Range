using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField, Header("Скорость полета пули")] private float speed;
    private Transform thisTransform;
    private Rigidbody rb;
    private void Awake()
    {
        thisTransform = transform;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(thisTransform.up * 10, ForceMode.Impulse);
    }

    // private void FixedUpdate()
    // {
    //     
    // }
}
