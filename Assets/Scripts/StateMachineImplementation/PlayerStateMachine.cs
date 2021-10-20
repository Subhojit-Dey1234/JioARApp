using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private void OnEnable()
    {
        PlayerCollisionDetection.OnCollisionEntered += DoDamage;
    }

    private void OnDisable()
    {
        PlayerCollisionDetection.OnCollisionEntered -= DoDamage;

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.AddComponent<BalanceState>();
            var _newState = GetComponent<BalanceState>();
            ChangeState(_newState);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.AddComponent<AttackState>();
            var _newState = GetComponent<AttackState>();
            ChangeState(_newState);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameObject.AddComponent<DefenceState>();
            var _newState = GetComponent<DefenceState>();
            ChangeState(_newState);
        }
    }

    
}
