using System.Collections;
using UnityEngine;

public class AttackState: State
{
    public override void Enter(float _safeDistance)
    {
        beyBladeParameters.CurentMode = "Attack Mode";
        //if(gameObject.CompareTag("Enemy"))
        //    StartCoroutine(AIModeSwitch(Random.Range(beyBladeParameters.stateChangeGapLow, beyBladeParameters.stateChangeGapHigh), _safeDistance));
    }
    public override void TakeDamage(float _dmg)
    {
        var _selfDmg = beyBladeParameters.attackMode_SelfDamage;
        staminaManager.ReduceStamina(_dmg * _selfDmg);
    }

    public override void DoDamage(StateMachine _enemyStateMachine)
    {
        var _enemyDmg = beyBladeParameters.attackMode_EnemyDamage;
        _enemyStateMachine.TakeDamage(_enemyDmg);
    }
    public override float CalculateMovementMultiplier()
    {
        return beyBladeParameters.attackMode_MovementMultiplier;
    }
    public override float CalculateStaminaLoss()
    {
        return beyBladeParameters.attackMode_StaminaLoss;
    }
    public override Vector3 Move(float _safeDistance)
    {
        var _seperation = player.transform.position - transform.position;
        _seperation.Normalize();
        return _seperation;
    }
    public override IEnumerator AIModeSwitch(float _time, float _safeDistance)
    {
        yield return new WaitForSeconds(_time);
        Debug.Log($"Inside Mode Swtch of {gameObject}");
        var _seperation = player.transform.position - transform.position;
        if (player.GetComponent<BeyBladeParameters>().CurentMode == "Attack Mode")
        {
            if (_seperation.magnitude <= _safeDistance)
            {
                int _rand = Random.Range(0, 2);
                if (_rand == 0)
                    NewAttackState();
                else
                    NewDefenceState();
            }
            else
            {
                int _rand = Random.Range(0, 3);
                if (_rand == 0)
                    NewAttackState();
                else if (_rand == 1)
                    NewDefenceState();
                else
                    NewBalanceState();
            }
        }
        if (player.GetComponent<BeyBladeParameters>().CurentMode == "Defence Mode")
        {
            if (_seperation.magnitude <= _safeDistance)
            {
                int _rand = Random.Range(0, 2);
                if (_rand == 0)
                    NewAttackState();
                else
                    NewDefenceState();
            }
            else
            {
                int _rand = Random.Range(0, 2);
                if (_rand == 0)
                    NewDefenceState();
                else
                    NewBalanceState();
            }
        }
        if (player.GetComponent<BeyBladeParameters>().CurentMode == "Balance Mode")
        {
            int _rand = Random.Range(0, 3);
            if (_rand == 0)
                NewAttackState();
            else if (_rand == 1)
                NewDefenceState();
            else
                NewBalanceState();
        }
        StartCoroutine(AIModeSwitch(Random.Range(beyBladeParameters.stateChangeGapLow, beyBladeParameters.stateChangeGapHigh), _safeDistance));
    }

    protected override void NewAttackState()
    {
        Destroy(this);
        gameObject.AddComponent<AttackState>();
        stateMachine.ChangeState(GetComponent<AttackState>());
    }

    protected override void NewDefenceState()
    {
        Destroy(this);
        gameObject.AddComponent<DefenceState>();
        stateMachine.ChangeState(GetComponent<DefenceState>());
    }

    protected override void NewBalanceState()
    {
        Destroy(this);
        gameObject.AddComponent<BalanceState>();
        stateMachine.ChangeState(GetComponent<BalanceState>());
    }

}
