using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.x != 0){
            transform.rotation = Quaternion.AngleAxis(0,transform.up);
        }
        if(controller.isGrounded){
            gravity = Vector3.zero;
        }
        else{
            gravity = new Vector3(0f,-9.8f,0f);
        }

        var forwardMovement_rb = -new Vector3(0,1f,0f) + gravity;
        controller.Move(forwardMovement_rb * 10f * Time.deltaTime);


        

        
        if( Input.GetKey(KeyCode.W)){
            var forwardMovement = new Vector3(0,0f,1f) + gravity;
            controller.Move(forwardMovement * 40f * Time.deltaTime);
        }
        if( Input.GetKey(KeyCode.S)){
            var forwardMovement = -new Vector3(0,0f,1f) + gravity;
            controller.Move(forwardMovement * 40f * Time.deltaTime);
        }
        if( Input.GetKey(KeyCode.D)){
            var forwardMovement = new Vector3(1,0f,0f) + gravity;
            controller.Move(forwardMovement * 40f * Time.deltaTime);
        }
        if( Input.GetKey(KeyCode.A)){
            var forwardMovement = -new Vector3(1,0f,0f) + gravity;
            controller.Move(forwardMovement * 40f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Space)){
            controller.Move(new Vector3(0,1f,0) * jump * 10f * Time.deltaTime);
        }
    }
}
