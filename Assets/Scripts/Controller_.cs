using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_ : MonoBehaviour
{
    [Range(0.0f,100f)]
    public float speed = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0,0,1) * speed * Time.deltaTime;
            
        }
        if( Input.GetKey(KeyCode.S)){
            transform.position -= new Vector3(0,0,1) * speed * Time.deltaTime;
        }
        if( Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(1,0,0) * speed * Time.deltaTime;
        }
        if( Input.GetKey(KeyCode.A)){
            transform.position -= new Vector3(1,0,0) * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Space)){
            transform.position += new Vector3(0,1,0) * speed * Time.deltaTime;
        }
    }
}
