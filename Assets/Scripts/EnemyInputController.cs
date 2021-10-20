using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputController : MonoBehaviour
{
    private float m_movementX, m_movementZ;
    public float MovementX
    {
        get { return m_movementX; }
    }
    public float MovementZ
    {
        get { return m_movementZ; }
    }
    private Vector3 m_velocityInput;
    public Vector3 VelocityInput
    {
        get { return m_velocityInput; }
    }
    // Update is called once per frame
    void Update()
    {
        KeyBoardMovement();
        SetInputVelocity();
    }

    private void SetInputVelocity()
    {
        m_velocityInput = new Vector3(m_movementX, 0f, m_movementZ);
    }

    private void KeyBoardMovement()
    {
        if(Input.GetKey(KeyCode.K))
        {
            m_movementZ = -1f;
        }
        else if(Input.GetKey(KeyCode.I))
        {
            m_movementZ = 1f;
        }
        else
        {
            m_movementZ = 0f;
        }
        if(Input.GetKey(KeyCode.J))
        {
            m_movementX = -1f;
        }
        else if(Input.GetKey(KeyCode.L))
        {
            m_movementX = 1f;
        }
        else
        {
            m_movementX = 0f;
        }
    }
}
