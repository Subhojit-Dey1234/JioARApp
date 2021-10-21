using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private StaminaManager staminaManager;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        staminaManager = player.GetComponent<StaminaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("{0:0.00}",staminaManager.CurrentStamina);
    }
}
