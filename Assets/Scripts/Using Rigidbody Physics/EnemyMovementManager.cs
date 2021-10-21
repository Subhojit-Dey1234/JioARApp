using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
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
    private float safeDistance;
    private RotationManager rotationManager;
    private StateMachine stateMachine;
    private GameObject player;
    private bool shouldMove = false;
    private void Awake()
    {
        rotationManager = GetComponent<RotationManager>();
        stateMachine = GetComponent<StateMachine>();
        player = GameObject.FindGameObjectWithTag("Player");
        safeDistance = GetComponent<BeyBladeParameters>().SafeDistance;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!shouldMove)
            return;
        if (rotationManager.IsRotationStopped)
            return;
        moveDirection = CalculateAIMovement();
        var _moveMultipier = stateMachine.CalculateMovementMultiplier();
        rbd.AddForce(moveDirection * moveForce * _moveMultipier, ForceMode.VelocityChange);
        velocity = rbd.velocity.magnitude;
    }

    private Vector3 CalculateAIMovement()
    {
        var _moveDirection = stateMachine.Move(safeDistance);
        _moveDirection.y = 0;
        _moveDirection.Normalize();
        return _moveDirection;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Stadium"))
        {
            Debug.Log($" {gameObject.name} Hit ground");
            rbd.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            shouldMove = true;
        }
    }

}

