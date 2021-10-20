using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField]
    private float initalAngularSpeed = 100000000;
    [SerializeField]
    private Vector3 m_EulerAngleVelocity = Vector3.up;
    [SerializeField]
    private float retardingTorque = 1000000;
    private bool m_isRotationStopped = false;
    private Rigidbody rbd;
    public bool IsRotationStopped
    {
        get { return m_isRotationStopped; }
    }
    private float angularSpeed;
    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
        angularSpeed = initalAngularSpeed;
    }
    private void FixedUpdate()
    {
        Rotate();
    }
    private void Update()
    {
        Retard();
        
    }

    private void Retard()
    {
        angularSpeed -= Time.deltaTime * retardingTorque;

        if(angularSpeed <= 0)
        {
            angularSpeed = 0;
            m_isRotationStopped = true;
        }    
    }

    private void Rotate()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime * angularSpeed);
        rbd.MoveRotation(rbd.rotation * deltaRotation);
    }
}
