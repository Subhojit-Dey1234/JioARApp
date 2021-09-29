using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.0f,100f)]
    public float speed = 20f;
    [Range(0.0f,100f)]
    public float jump = 10f;
    public float torque = 20f;
    public GameObject target;
    public float rotationSpeed = 20f;
    Rigidbody rb;
    CharacterController controller;
    Vector3 gravity;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {

        transform.RotateAround(transform.position,transform.up,speed);
        // rb.AddTorque(transform.up * torque * speed);


        if(transform.rotation.x != 0){
            transform.rotation = Quaternion.AngleAxis(0,transform.up);
        }
        if(!controller){
            return;
        }
        if(controller.isGrounded){
            gravity = Vector3.zero;
        }
        else{
            gravity = new Vector3(0f,-9.8f,0f);
        }

        var forwardMovement_rb = -new Vector3(0,1f,0f) + gravity;
        controller.Move(forwardMovement_rb * 10f * Time.deltaTime);
    }
}
