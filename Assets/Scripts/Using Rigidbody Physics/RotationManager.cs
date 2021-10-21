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
    private float retardingTorque = 500000;
    private bool m_isRotationStopped = false;
    private Rigidbody rbd;
    private StaminaManager staminaManager;
    public bool IsRotationStopped
    {
        get { return m_isRotationStopped; }
    }
    private float angularSpeed;
    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
        staminaManager = GetComponent<StaminaManager>();
        angularSpeed = initalAngularSpeed;
    }
    private void FixedUpdate()
    {
        Rotate();
    }
 
    private void Rotate()
    {
        if (m_isRotationStopped)
            return;
        if (angularSpeed <= 0)
        {
            m_isRotationStopped = true;
            return;
        }
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime * angularSpeed * staminaManager.CurrentStamina);
        rbd.MoveRotation(rbd.rotation * deltaRotation);
    }
}
