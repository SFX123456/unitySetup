using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   [SerializeField] private float speed;
    private Rigidbody rb;
    private bool shouldApplyForce;
    private void Start()
    {
        shouldApplyForce = true;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!shouldApplyForce) return;
        Debug.Log("apply force");
        rb.AddForce(transform.forward * speed * 10000);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit an object");
        shouldApplyForce = false;
    }
}
