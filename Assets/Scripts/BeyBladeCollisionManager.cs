using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyBladeCollisionManager : MonoBehaviour
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
    private CharacterController character;
    private GameObject player;
    private Vector3 m_velocityImpact;

    public Vector3 VelocityImpact
    {
        get { return m_velocityImpact; }
    }

    private void Start()
    {
        character = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Enemy");
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        AddImpulse(other.gameObject);
    //    }
    //}
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
        DecellerateVelocity();
    }

    private void OnBeyBladeCollided()
    {
        Vector3 _origin = transform.position;
        for (int i = 0; i <= 360; i++)
        {
            float _radian = i * Mathf.PI / 180f;
            Vector3 _direction = new Vector3(Mathf.Cos(_radian), 0f, Mathf.Sin(_radian));
            Debug.Log("Spherecasting");
            if (Physics.SphereCast(_origin, sphereRadius, _direction, out RaycastHit hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Collide))
            {
                if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    AddImpulse(hitInfo.transform.gameObject);
                }
            }
        }
    }

    private void AddImpulse(GameObject _otherObject)
    {
        Debug.Log(_otherObject.name);
        Vector3 _lineOfImpact = Vector3.Normalize(transform.position - _otherObject.transform.position);
        Vector3 _otherVelocity = _otherObject.GetComponent<CharacterController>().velocity;
        if (Vector3.Dot(_otherVelocity, _lineOfImpact) > 0f)
            ChangeImpactVelocity(_lineOfImpact, _otherVelocity);
        else
            return;
    }

    private void ChangeImpactVelocity(Vector3 _lineOfImpact, Vector3 _otherVelocity)
    {
        m_velocityImpact = _lineOfImpact * impulseMultiplier * Vector3.Dot(_otherVelocity, _lineOfImpact);
    }
    private void DecellerateVelocity()
    {
        m_velocityImpact *= Mathf.Exp(Time.deltaTime * -1 * descellerationRate);
    }
}
