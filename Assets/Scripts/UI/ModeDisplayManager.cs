using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModeDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private BeyBladeParameters beyBladeParameters;
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        beyBladeParameters = player.GetComponent<BeyBladeParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = beyBladeParameters.CurentMode.ToString();
    }
}
