using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BeyBladeInputController))]
public class BeyBladeMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private CharacterController characterController;
    private Vector3 m_velocityImpact;
    [HideInInspector]
    public Vector3 VelocityImpact
    {
        set { m_velocityImpact = value; }
    }
    void Start()
    {

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var _velocityInput = GetComponent<BeyBladeInputController>().VelocityInput;
        var _velocityImpact = m_velocityImpact;
        var _netVelocity = _velocityImpact + _velocityInput;
        characterController.SimpleMove(Time.deltaTime * speed * _netVelocity);

    }
}
