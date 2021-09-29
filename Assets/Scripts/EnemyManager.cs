using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject attacker;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dist = transform.position - attacker.transform.position;
        transform.position = Vector3.MoveTowards(transform.position,attacker.transform.position,dist.magnitude * Time.deltaTime);
    }
}
