public class DefenceState: State
{
    public override void TakeDamage(float _dmg)
    {
        var _selfDamage = beyBladeParameters.defenceMode_SelfDamage;
        staminaManager.ReduceStamina(_dmg * _selfDamage);
    }

    public override void DoDamage(StateMachine _enemyStateMachine)
    {
        var _enemyDmg = beyBladeParameters.defenceMode_EnemyDamage;
        _enemyStateMachine.TakeDamage(_enemyDmg);
    }
    public override float CalculateMovementMultiplier()
    {
        return beyBladeParameters.defenceMode_MovementMultiplier;
    }
}