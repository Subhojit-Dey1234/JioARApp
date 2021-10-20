using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State: MonoBehaviour
{
    public delegate void DamageHandler(int _dmg, GameObject _gameObject);
    public static event DamageHandler OnDamageDone;
    protected BeyBladeParameters beyBladeParameters;
    protected StaminaManager staminaManager;
    protected StateMachine stateMachine;
    private void Start()
    {
        beyBladeParameters = GetComponent<BeyBladeParameters>();
        stateMachine = GetComponent<StateMachine>();
        staminaManager = GetComponent<StaminaManager>();
    }
    public virtual void TakeDamage(float _dmg)
    {
        
    }

    public virtual void DoDamage( StateMachine _enemyStateMachine)
    {
    }

    public virtual float CalculateMovementMultiplier()
    {
        return 0;
    }

}
