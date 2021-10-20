using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BeyBladeParameters: MonoBehaviour

{
    [SerializeField]
    [Range(0,2)]
    public float attackMode_MovementMultiplier;
    [SerializeField]
    public float attackMode_EnemyDamage;
    [SerializeField]
    public float attackMode_SelfDamage;
    [SerializeField]
    public float attackMode_StaminaLoss;

    [SerializeField]
    [Range(0, 2)]
    public float defenceMode_MovementMultiplier;
    [SerializeField]
    public float defenceMode_EnemyDamage;
    [SerializeField]
    public float defenceMode_SelfDamage;
    [SerializeField]
    public float defenceMode_StaminaLoss;

    [SerializeField]
    [Range(0, 2)]
    public float balanceMode_MovementMultiplier;
    [SerializeField]
    public float balanceMode_EnemyDamage;
    [SerializeField]
    public float balanceMode_SelfDamage;
    [SerializeField]
    public float balanceMode_StaminaLoss;
}
