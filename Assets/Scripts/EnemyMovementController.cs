using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private CharacterController character;
    private void Start()
    {
        character = GetComponent<CharacterController>();
    }
    void Update()
    {
        var _velocityInput = GetComponent<EnemyInputController>().VelocityInput;
        var _velocityImpact = GetComponent<EnemyCollisionManager>().VelocityImpact;
        var _velocity = _velocityImpact + _velocityInput;
        character.SimpleMove(Time.deltaTime * speed * _velocity);
    }
}
