using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour
{
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float sphereRadius = 0.01f;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float impulseMultiplier = 100;
    [SerializeField]
    private float descellerationRate = 10;
    [SerializeField]
    private float replaySoundTime = 0.7f;
    [SerializeField]
    private float collisionGap = 0.1f;
    private CharacterController character;
    private GameObject player;
    private bool shouldSphereCast = false;
    private bool isCollisionPlaying = false;
    private bool shouldCollide = false;
    private Vector3 m_enemyImpactVelocity;
    public Vector3 VelocityImpact
    {
        get { return m_enemyImpactVelocity; }
    }
    private Vector3 m_playerImpactVelocity;
    public delegate void PlayerVelocityChangeHandler(Vector3 _playerImpactVelocity);
    public static event PlayerVelocityChangeHandler OnPlayerVelocityChange;
    private void Start()
    {
        character = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            shouldSphereCast = true;
            shouldCollide = true;
            Debug.Log("Start Spherecast");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Stop Spherecast");
            shouldSphereCast = false;
            shouldCollide = false;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        AddImpulse(collision.gameObject);
    //    }
    //}
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("Player"))
    //    {
    //        AddImpulse(hit.gameObject);
    //    }
    //}
    private void Update()
    {
        OnBeyBladeCollided();
        m_enemyImpactVelocity = DecellerateVelocity(m_enemyImpactVelocity);
        m_playerImpactVelocity = DecellerateVelocity(m_playerImpactVelocity);
        SetPlayerImpactVelocity();
    }

    private void SetPlayerImpactVelocity()
    {
        player.GetComponent<BeyBladeMovementController>().VelocityImpact = m_playerImpactVelocity;
    }

    private void OnBeyBladeCollided()
    {
        if (!shouldSphereCast && !shouldCollide)
        {
            return;
        }
        Vector3 _origin = transform.position;
        for (int i = 0; i <= 360; i++)
        {
            float _radian = i * Mathf.PI / 180f;
            Vector3 _direction = new Vector3(Mathf.Cos(_radian), 0f, Mathf.Sin(_radian));
            if (Physics.SphereCast(_origin, sphereRadius, _direction, out RaycastHit hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Collide))
            {
                if (hitInfo.transform.gameObject.CompareTag("Player"))
                {
                    Collide(hitInfo.transform.gameObject);
                    PlayAudio();
                }
            }
        }
    }

    private void PlayAudio()
    {
        if(!isCollisionPlaying)
        {
            AudioManager.Instance.PlaySoundOneShot("BeyBladeHit");
            isCollisionPlaying = true;
            StartCoroutine(ReplayHitSound( replaySoundTime));
        }
    }

    IEnumerator ReplayHitSound(float _time)
    {
        yield return new WaitForSeconds(_time);
        isCollisionPlaying = false;
    }

    private void Collide(GameObject _playerObject)
    {
        StartCoroutine(CollisionDone());
        Debug.Log(_playerObject.name);
        Vector3 _lineOfImpact = Vector3.Normalize(transform.position - _playerObject.transform.position);
        Vector3 _playerInitialVelocity = _playerObject.GetComponent<CharacterController>().velocity;
        Vector3 _enemyInitialVelocity = GetComponent<CharacterController>().velocity;
        Vector3 _relativeVelocity = _playerInitialVelocity - _enemyInitialVelocity;
        if (Vector3.Dot(_relativeVelocity, _lineOfImpact) > 0f)
        {
            //ChangeEnemyImpactVelocity(_lineOfImpact, _relativeVelocity);
            m_enemyImpactVelocity = CalculateFinalVelocityAfterCollision(_enemyInitialVelocity,_playerInitialVelocity,_lineOfImpact);
            m_playerImpactVelocity = CalculateFinalVelocityAfterCollision(_playerInitialVelocity,_enemyInitialVelocity,_lineOfImpact);     
        }
        else
            return;
    }

    private Vector3 CalculateFinalVelocityAfterCollision(Vector3 _passedObjectInitialVelocity, Vector3 _otherObjectrInitialVelocity, Vector3 _lineOfImpact)
    {
        Vector3 _passedObjectFinalVelocityPerpendicular = _passedObjectInitialVelocity - Vector3.Dot(_passedObjectInitialVelocity, _lineOfImpact)*_lineOfImpact;
        Vector3 _passedObjectFinalVelocityParallel = Vector3.Dot(_otherObjectrInitialVelocity, _lineOfImpact) * _lineOfImpact *impulseMultiplier;
        if(_passedObjectFinalVelocityParallel.magnitude == 0)
        {
            Debug.Log("Final velocity = 0");
            _lineOfImpact.Normalize();
            _passedObjectFinalVelocityParallel = _lineOfImpact * 0.1f;
        }
        return ( _passedObjectFinalVelocityPerpendicular + _passedObjectFinalVelocityParallel);
    }

    private void ChangeEnemyImpactVelocity(Vector3 _lineOfImpact, Vector3 _otherVelocity)
    {
        m_enemyImpactVelocity = _lineOfImpact * impulseMultiplier * Vector3.Dot(_otherVelocity,_lineOfImpact) ;
    }
    private Vector3 DecellerateVelocity(Vector3 _velocity)
    {
        _velocity *= Mathf.Exp(Time.deltaTime * -1 * descellerationRate);
        return _velocity;
    }

    IEnumerator CollisionDone()
    {
        shouldCollide = false;
        yield return new WaitForSeconds(collisionGap);
        shouldCollide = true;
    }
}
