using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public delegate void DeathHandler(GameObject gameObject);
    public static event DeathHandler OnDied;
    [SerializeField]
    private float startingStamina = 100f;
    [SerializeField]
    private float staminaReductionRate = 5f;
    private float currentStamina;
    public float CurrentStamina
    {
        get { return currentStamina; }
    }

    private StateMachine stateMachine;
    private bool hasDied = false;
    void Start()
    {
        Time.timeScale = 1f;
        stateMachine = GetComponent<StateMachine>();
        currentStamina = startingStamina;
    }
    private void Update()
    {
        Retard();
    }
    private void Retard()
    {
        if (hasDied)
            return;
        currentStamina -= Time.deltaTime * (stateMachine.CalculateStaminaLoss() + staminaReductionRate);

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            hasDied = true;
            OnDied?.Invoke(gameObject);    
        }
    }
    public void ReduceStamina(float _dmg)
    {
        currentStamina -= _dmg;
        if(currentStamina <= 0)
        {
            Debug.Log("Stamina = 0");
            currentStamina = 0;
            hasDied = true;
            Time.timeScale = 0f;
            OnDied?.Invoke(gameObject);
        }
    }
}
