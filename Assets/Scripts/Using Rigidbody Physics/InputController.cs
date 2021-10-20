using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;

public class InputController : MonoBehaviour//,  ITouchHandler
{
    private Vector3 m_moveDirection;
    [SerializeField]
    KeyCode horizontalPlus, verticalPlus;
    [SerializeField]
    KeyCode horizontalMinus, verticalMinus;
    private float m_movementZPositive;
    private float m_movementZNegetive;
    private float m_movementXPositive;
    private float m_movementXNegetive;

    [HideInInspector]
    public Vector3 MoveDirection
    {
        get { return m_moveDirection; }
    }
    private void Start()
    {
        m_moveDirection = Vector3.zero;
        //JMRInputManager.Instance.AddGlobalListener(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        KeyBoardMovement();
        //Move();
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

    private void Move()
    {
        // var _movementX = m_movementXNegetive + m_movementXPositive;
        //var _movementZ = m_movementZNegetive + m_movementZPositive;
         
        m_moveDirection = new Vector3(JMRInteraction.GetTouch().x, 0f, JMRInteraction.GetTouch().y);
        m_moveDirection.Normalize();
    }
    //public void OnSwipeLeft(SwipeEventData eventData, float value)
    //{
    //    Debug.Log("Left swipe");
    //    m_movementXNegetive = value;
    //}

    //public void OnSwipeRight(SwipeEventData eventData, float value)
    //{
    //    Debug.Log("Right swipe");  
    //    m_movementXPositive = value;
    //}

    //public void OnSwipeUp(SwipeEventData eventData, float value)
    //{
    //    Debug.Log("Upward swipe");
    //    m_movementZPositive = value;
    //}

    //public void OnSwipeDown(SwipeEventData eventData, float value)
    //{
    //    Debug.Log("Downward swipe");
    //    m_movementZNegetive = value;
    //}

    //public void OnSwipeStarted(SwipeEventData eventData)
    //{
    //    Debug.Log("Started swipe");
    //    m_movementXPositive = 0;
    //    m_movementXNegetive = 0;
    //    m_movementZPositive = 0;
    //    m_movementZNegetive = 0;
    //}

    //public void OnSwipeUpdated(SwipeEventData eventData, Vector2 swipeData)
    //{
    //    var _xData = swipeData.x;
    //    var _zData = swipeData.y;
    //    if (_xData >= 0)
    //        m_movementXPositive = _xData;
    //    else
    //        m_movementXNegetive = _xData;

    //    if (_zData >= 0)
    //        m_movementZPositive = _zData;
    //    else
    //        m_movementZNegetive = _zData;

    //}

    //public void OnSwipeCompleted(SwipeEventData eventData)
    //{
    //    Debug.Log("Completed swipe");
    //    m_movementXPositive = 0;
    //    m_movementXNegetive = 0;
    //    m_movementZPositive = 0;
    //    m_movementZNegetive = 0;
    //}

    //public void OnSwipeCanceled(SwipeEventData eventData)
    //{
    //    Debug.Log("Cancelled swipe");
    //}

    public void OnTouchStart(TouchEventData eventData, Vector2 TouchData)
    {

        var _xData = TouchData.x;
        var _zData = TouchData.y;
        if (_xData >= 0)
            m_movementXPositive = _xData;
        else
            m_movementXNegetive = _xData;

        if (_zData >= 0)
            m_movementZPositive = _zData;
        else
            m_movementZNegetive = _zData;
        Debug.Log("Touch started");
    }

    public void OnTouchStop(TouchEventData eventData, Vector2 TouchData)
    {
        m_movementXPositive = 0;
        m_movementXNegetive = 0;
        m_movementZPositive = 0;
        m_movementZNegetive = 0;
    }

    public void OnTouchUpdated(TouchEventData eventData, Vector2 TouchData)
    {
        var _xData = TouchData.x;
        var _zData = TouchData.y;
        if (_xData >= 0)
            m_movementXPositive = _xData;
        else
            m_movementXNegetive = _xData;

        if (_zData >= 0)
            m_movementZPositive = _zData;
        else
            m_movementZNegetive = _zData;
        Debug.Log("Touch updating");
    }
}
