using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State state;
    private void Awake()
    {
        gameObject.AddComponent<BalanceState>();
    }
    private void Start()
    {
        state = GetComponent<BalanceState>();
    }
    public void TakeDamage(float _dmg)
    {
        state.TakeDamage(_dmg);
    }
    public void DoDamage( GameObject _enemyObject)
    {
        state.DoDamage( _enemyObject.GetComponent<StateMachine>());
    }

    public float CalculateMovementMultiplier()
    {
        return state.CalculateMovementMultiplier();
    }

    public void ChangeState(State _newState)
    {
        Destroy(state);
        this.state = _newState;
    }
}
