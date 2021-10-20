public class AttackState: State
{
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
}
