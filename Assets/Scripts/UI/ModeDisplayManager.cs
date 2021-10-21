using UnityEngine;
using UnityEngine.UI;

public class ModeDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private BeyBladeParameters beyBladeParameters;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        beyBladeParameters = player.GetComponent<BeyBladeParameters>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = beyBladeParameters.CurentMode.ToString();
    }
}
