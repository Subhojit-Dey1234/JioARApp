using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private StaminaManager staminaManager;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        staminaManager = player.GetComponent<StaminaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = staminaManager.CurrentStamina.ToString();
    }
}
