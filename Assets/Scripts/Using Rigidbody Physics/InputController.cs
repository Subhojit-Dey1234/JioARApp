using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;

public class InputController : MonoBehaviour,IFn1Handler
{
    private Vector3 m_moveDirection;
    [SerializeField]
    KeyCode horizontalPlus, verticalPlus;
    [SerializeField]
    KeyCode horizontalMinus, verticalMinus;
    [SerializeField]
    LayerMask stadiumLayer;
    [SerializeField]
    private float maxDistanceOfRay = 100f;
    private float m_movementZPositive;
    private float m_movementZNegetive;
    private float m_movementXPositive;
    private float m_movementXNegetive;
    private bool isMoving = false;
    [HideInInspector]
    public Vector3 MoveDirection
    {
        get { return m_moveDirection; }
    }
    private void Start()
    {
        m_moveDirection = Vector3.zero;
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //KeyBoardMovement();
        RayMovement();
    }

    private void RayMovement()
    {
        if(!isMoving)
        {
            m_moveDirection = Vector3.zero;
            return;
        }
        Ray pointerRay = JMRPointerManager.Instance.GetCurrentRay();
        if(Physics.Raycast(pointerRay, out RaycastHit hit, maxDistanceOfRay,stadiumLayer))
        {

            var _tempMoveDirection = hit.point - transform.position;
            m_moveDirection = new Vector3(_tempMoveDirection.x, 0, _tempMoveDirection.z);
            m_moveDirection.Normalize();
        }
        else
        {
            m_moveDirection = Vector3.zero;
        }


    }

    private void KeyBoardMovement()
    {
        m_movementZPositive= Input.GetKey(verticalPlus) ? 1:0 ;
        m_movementZNegetive= Input.GetKey(verticalMinus) ? -1:0;
        var _movementZ = m_movementZNegetive + m_movementZPositive;
        m_movementXPositive = Input.GetKey(horizontalPlus) ? 1 : 0;
        m_movementXNegetive = Input.GetKey(horizontalMinus) ? -1 : 0;
        var _movementX = m_movementXNegetive + m_movementXPositive;
        m_moveDirection = new Vector3(_movementX, 0f, _movementZ);
        m_moveDirection.Normalize();
    }
    public void OnFn1Action()
    {
        Debug.Log("Fn1 pressed");
        if (isMoving)
            isMoving = false;
        else
            isMoving = true;
    }

}
