using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rbd;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private float moveForce = 100;
    [SerializeField]
    private float collisionImpulseMultiplier = 1000f;
    private InputController playerInputController;
    private RotationManager rotationManager;
    private PlayerStateMachine playerStateMachine;
    private void Awake()
    {
        playerInputController = GetComponent<InputController>();
        rotationManager = GetComponent<RotationManager>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (rotationManager.IsRotationStopped)
            return;
        moveDirection = playerInputController.MoveDirection;
        var _moveMultipier = playerStateMachine.CalculateMovementMultiplier();
        rbd.AddForce(moveDirection * moveForce * _moveMultipier, ForceMode.VelocityChange);
        velocity = rbd.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Stadium"))
        {
            Debug.Log($" {gameObject.name} Hit ground");
            rbd.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
    }

}
