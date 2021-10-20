using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public delegate void CollisionEnterHandler(GameObject gameObject);
    public delegate void CollisionExitHandler();
    public static event CollisionEnterHandler OnCollisionEntered;
    public static event CollisionExitHandler OnCollisionExited;
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
    private float collisionGap = 0.5f;
    private bool isColliding = false;
    private void Awake()
    {
        rbd = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        var _other = _collision.gameObject;
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody _otherRBD = _other.GetComponent<Rigidbody>();
            //Debug.Log("Collided");
            var _lineOfImpact = (transform.position - _other.transform.position);
            _lineOfImpact.Normalize();
            //Debug.Log($"Velocity of {_other.name} = {_otherRBD.velocity.magnitude}");
            //Debug.Log($"Velocity of {gameObject.name} = {rbd.velocity.magnitude}");
            //var _forceMagnitude = Vector3.Dot(other.GetComponent<Rigidbody>().velocity - rbd.velocity, _lineOfImpact);
            //Debug.Log($"Force magnitde on {gameObject.name} = {_forceMagnitude}");
            Vector3 _relVel = _otherRBD.velocity - rbd.velocity;
            _relVel.Normalize();
            //Debug.Log($"LineOfImpact = {_lineOfImpact}");
            //Debug.Log($"RelVel = {_relVel}");
            //Debug.Log($"Dot product = {Vector3.Dot(_relVel, _lineOfImpact)}");
            if (Vector3.Dot(_relVel, _lineOfImpact) >= 0.1f)
            {
                rbd.AddForce(_lineOfImpact * collisionImpulseMultiplier, ForceMode.Impulse);
                _otherRBD.AddForce(-1 * _lineOfImpact * collisionImpulseMultiplier, ForceMode.Impulse);
            }
            Instantiate(spark, _collision.contacts[0].point, Quaternion.identity);
            AudioManager.Instance.PlaySoundOneShot("BeyBladeHit");
            OnCollisionEntered?.Invoke(_other);
            isColliding = true;
            StartCoroutine(ContinueCollisions(_collision));
        }
    }
    private void OnCollisionExit(Collision _collision)
    {
        var _other = _collision.gameObject;
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            //OnCollisionExited?.Invoke();
            isColliding = false;
        }
    }

    IEnumerator ContinueCollisions(Collision _collision)
    {
        yield return new WaitForSeconds(collisionGap);
        if(isColliding)
        {
            AudioManager.Instance.PlaySoundOneShot("BeyBladeHit");
            Instantiate(spark, _collision.contacts[0].point, Quaternion.identity);
            OnCollisionEntered?.Invoke(_collision.gameObject);
            StartCoroutine(ContinueCollisions(_collision));
        }
    }
    //void DetectCollision()
    //{
    //    //Debug.Log("Collisions detecting");
    //    int maxColliders = 10;
    //    Collider[] hitColliders = new Collider[maxColliders];
    //    int numColliders = Physics.OverlapSphereNonAlloc(model.transform.position, cCollider.radius, hitColliders);
    //    Debug.DrawRay(model.transform.position, model.transform.forward * cCollider.radius , Color.red, 2f);
    //    Debug.Log($"{hitColliders}");
    //    for (int i = 0; i < numColliders; i++)
    //    {
    //        if(hitColliders[i].gameObject.CompareTag("Enemy"))
    //        {
    //            var _other = hitColliders[i].gameObject;
    //            Rigidbody _otherRBD = _other.GetComponent<Rigidbody>();
    //            Debug.Log("Collided");
    //            var _lineOfImpact = (transform.position - _other.transform.position);
    //            _lineOfImpact.Normalize();
    //            Debug.Log($"Velocity of {_other.name} = {_otherRBD.velocity.magnitude}");
    //            Debug.Log($"Velocity of {gameObject.name} = {rbd.velocity.magnitude}");
    //            //var _forceMagnitude = Vector3.Dot(other.GetComponent<Rigidbody>().velocity - rbd.velocity, _lineOfImpact);
    //            //Debug.Log($"Force magnitde on {gameObject.name} = {_forceMagnitude}");
    //            Vector3 _relVel = _otherRBD.velocity - rbd.velocity;
    //            _relVel.Normalize();
    //            Debug.Log($"LineOfImpact = {_lineOfImpact}");
    //            Debug.Log($"RelVel = {_relVel}");
    //            Debug.Log($"Dot product = {Vector3.Dot(_relVel, _lineOfImpact)}");
    //            if (Vector3.Dot(_relVel, _lineOfImpact) >= 0.9f)
    //            {
    //                rbd.AddForce(_lineOfImpact * collisionImpulseMultiplier, ForceMode.Impulse);
    //                _otherRBD.AddForce(-1 * _lineOfImpact * collisionImpulseMultiplier, ForceMode.Impulse);
    //            }
    //            Instantiate(spark, 0.5f * (transform.position + _other.transform.position), Quaternion.identity);
    //            AudioManager.Instance.PlaySoundOneShot("BeyBladeHit");
    //        }
    //    }
    //}
}
