using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBloack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject marker;
    [Range(0,100f)]
    public float m_Thrust = 2f;
    Vector3 impact = Vector3.zero;

    void AddImpact(Vector3 dir, float force, float mass)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;


    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.CompareTag("Enemy"))
        {
            Rigidbody r = col.gameObject.GetComponent<Rigidbody>();
            CharacterController enemy = r.gameObject.GetComponent<CharacterController>();
            foreach (ContactPoint contact in col.contacts)
            {
                Vector3 dir =  contact.point - marker.transform.position;
                Vector3 val = dir.normalized * m_Thrust * Time.deltaTime;
                r.AddForceAtPosition(val,contact.point,ForceMode.Impulse);
            }

        }
    }


    
}
