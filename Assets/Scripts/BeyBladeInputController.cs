using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyBladeInputController : MonoBehaviour
{
    private float m_movementX, m_movementZ;
    private Vector3 m_VelocityInput;
    [HideInInspector]
    public Vector3 VelocityInput
    {
        get { return m_VelocityInput; }
    }
    private void Start()
    {
        m_VelocityInput = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        KeyBoardMovement();
    }

    private void KeyBoardMovement()
    {
        m_movementZ = Input.GetAxis("Vertical");
        m_movementX = Input.GetAxis("Horizontal");
        m_VelocityInput = new Vector3(m_movementX, 0f, m_movementZ);
    }
}
