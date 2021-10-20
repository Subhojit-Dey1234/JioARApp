public class BalanceState : State
{
    public override void TakeDamage(float _dmg)
    {
        var _selfDamage = beyBladeParameters.balanceMode_SelfDamage;
        staminaManager.ReduceStamina(_dmg * _selfDamage);
    }

    public override void DoDamage(StateMachine _enemyStateMachine)
    {
        var _enemyDmg = beyBladeParameters.balanceMode_EnemyDamage;
        _enemyStateMachine.TakeDamage(_enemyDmg);
    }
    public override float CalculateMovementMultiplier()
    {
        return beyBladeParameters.balanceMode_MovementMultiplier;
    }
}