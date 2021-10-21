using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI UIText;
    private bool hasPoppedUp = false;
    private void OnEnable()
    {
        StaminaManager.OnDied += DisplayGameOver;
    }

    public void DisplayGameOver(GameObject _gameObject)
    {
        gameObject.SetActive(true);
        gameObject.transform.localScale = Vector3.one;
        Debug.Log("Game Over");
        if (hasPoppedUp)
            return;
        if(_gameObject.CompareTag("Player"))
        {
            UIText.text = "You lost";
        }
        if(_gameObject.CompareTag("Enemy"))
        {
            UIText.text = "You Won";
        }
        hasPoppedUp = true;
    }

    private void OnDisable()
    {
        StaminaManager.OnDied -= DisplayGameOver; 
    }
    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }
    public void PlayAgain()
    {
        GameManager.Instance.PlayGame();
    }
}
