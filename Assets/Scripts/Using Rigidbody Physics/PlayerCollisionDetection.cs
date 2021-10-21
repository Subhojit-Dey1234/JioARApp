using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public delegate void CollisionEnterHandler(GameObject gameObject);
    public delegate void CollisionExitHandler();
    public static event CollisionEnterHandler OnCollisionEntered;
    public static event CollisionExitHandler OnCollisionExited;
    public delegate void ForceHandler(GameObject gameObject, Vector3 _force);
    public static event ForceHandler OnForceAdded;
    [SerializeField]
    private float collisionImpulseMultiplier = 10000;
    [SerializeField]
    GameObject spark;
    private Rigidbody rbd;
    [SerializeField]
    private CapsuleCollider cCollider;
    [SerializeField]
    GameObject model;
    [SerializeField]
    private float collisionGaphWhileInContact = 1f;
    [SerializeField]
    private float timeLagBetweenTwoCollisions = 0.25f;
    private bool isColliding = false;
    private bool allowNextCollision = true;
    private StateMachine stateMachine;
    private void Awake()
    {
        rbd = GetComponent<Rigidbody>();
        stateMachine = GetComponent<StateMachine>();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (!allowNextCollision)
            return;
        var _other = _collision.gameObject;
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided");
            Rigidbody _otherRBD = _other.GetComponent<Rigidbody>();
            var _lineOfImpact = (transform.position - _other.transform.position);
            _lineOfImpact.Normalize();
            Vector3 _relVel = _otherRBD.velocity - rbd.velocity;
            _relVel.Normalize();
            if (Vector3.Dot(_relVel, _lineOfImpact) >= 0.1f)
            {
                rbd.AddForce(_lineOfImpact * collisionImpulseMultiplier, ForceMode.Impulse);
                _otherRBD.AddForce(-1 * _lineOfImpact * collisionImpulseMultiplier, ForceMode.Impulse);
            }
            Instantiate(spark, _collision.contacts[0].point, Quaternion.identity);
            AudioManager.Instance.PlaySoundOneShot("BeyBladeHit");
            stateMachine.DoDamage(_other);
            _other.GetComponent<StateMachine>().DoDamage(gameObject);
            isColliding = true;
            StartCoroutine(ContinueCollisions(_other, _collision.contacts[0].point));
            StartCoroutine(StartCountDonForNextCollision());
        }
    }

    IEnumerator StartCountDonForNextCollision()
    {
        allowNextCollision = false;
        yield return new WaitForFixedUpdate();
        allowNextCollision = true;
    }
    private void OnCollisionExit(Collision _collision)
    {
        var _other = _collision.gameObject;
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }

    IEnumerator ContinueCollisions(GameObject _enemyObject, Vector3 pointOfContact)
    {
        yield return new WaitForSeconds(collisionGaphWhileInContact);
        if(isColliding)
        {
            AudioManager.Instance.PlaySoundOneShot("BeyBladeHit");
            Instantiate(spark, pointOfContact, Quaternion.identity);
            Debug.Log($"Inside Coroutine: this object = {this.gameObject}, enemy object = {_enemyObject} ");
            stateMachine.DoDamage(_enemyObject);
            _enemyObject.GetComponent<StateMachine>().DoDamage(this.gameObject);
            StartCoroutine(ContinueCollisions(_enemyObject, pointOfContact));
        }
    }
}
