using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;

public class PlayerStateMachine : StateMachine, ISwipeHandler
{
    private void Awake()
    {
        gameObject.AddComponent<BalanceState>();
        state = GetComponent<BalanceState>();
        state.Enter(GetComponent<BeyBladeParameters>().SafeDistance);
    }
    void KeyBoardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.AddComponent<BalanceState>();
            var _newState = GetComponent<BalanceState>();
            ChangeState(_newState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.AddComponent<AttackState>();
            var _newState = GetComponent<AttackState>();
            ChangeState(_newState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameObject.AddComponent<DefenceState>();
            var _newState = GetComponent<DefenceState>();
            ChangeState(_newState);
        }

    }

    public void OnSwipeLeft(SwipeEventData eventData, float value)
    {
        Debug.Log("Defence Mode");
        gameObject.AddComponent<DefenceState>();
        var _newState = GetComponent<DefenceState>();
        ChangeState(_newState);
    }

    public void OnSwipeRight(SwipeEventData eventData, float value)
    {
        Debug.Log("Attack Mode");
        gameObject.AddComponent<AttackState>();
        var _newState = GetComponent<AttackState>();
        ChangeState(_newState);
    }

    public void OnSwipeUp(SwipeEventData eventData, float value)
    {
        Debug.Log("Balance Mode");
        gameObject.AddComponent<BalanceState>();
        var _newState = GetComponent<BalanceState>();
        ChangeState(_newState);
    }

    public void OnSwipeDown(SwipeEventData eventData, float value)
    {
    }

    public void OnSwipeStarted(SwipeEventData eventData)
    {
    }

    public void OnSwipeUpdated(SwipeEventData eventData, Vector2 swipeData)
    {
    }

    public void OnSwipeCompleted(SwipeEventData eventData)
    {
    }

    public void OnSwipeCanceled(SwipeEventData eventData)
    {
    }
}
