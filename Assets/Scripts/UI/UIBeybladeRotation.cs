using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBeybladeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.0f, 100f)]
    public float speed = 20f;
    void Update()
    {

        transform.RotateAround(transform.position, transform.up, speed);
        // rb.AddTorque(transform.up * torque * speed);
    }
}
