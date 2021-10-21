using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    private void Update()
    {
        //KeyBoardInput();
    }

    private void Awake()
    {
        gameObject.AddComponent<BalanceState>();
        Debug.Log("Inside Awake Of EnemyStateMAnager");
    }
    private void Start()
    {
        Debug.Log("Inside Start Of EnemyStateMAnager");
        state = GetComponent<BalanceState>();
        state.Enter(GetComponent<BeyBladeParameters>().SafeDistance);
    }
    void KeyBoardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameObject.AddComponent<BalanceState>();
            var _newState = GetComponent<BalanceState>();
            ChangeState(_newState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            gameObject.AddComponent<AttackState>();
            var _newState = GetComponent<AttackState>();
            ChangeState(_newState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            gameObject.AddComponent<DefenceState>();
            var _newState = GetComponent<DefenceState>();
            ChangeState(_newState);
        }
    }

    override public Vector3 Move(float _SafeDistance)
    {
        if (state != null)
            return state.Move(_SafeDistance);
        else
            return Vector3.zero;
    }
}
