using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    [SerializeField]
    private float startingStamina = 100f;
    [SerializeField]
    private float staminaReductionRate = 1f;
    private bool shouldReduceStamina = false;
    private float currentStamina;
    public float CurrentStamina
    {
        get { return currentStamina; }
    }
    void Start()
    {
        currentStamina = startingStamina;
    }

    private void ReduceStamina()
    {
        currentStamina -= staminaReductionRate;
    }
    public void ReduceStamina(float _dmg)
    {
        currentStamina -= _dmg;
    }
}
