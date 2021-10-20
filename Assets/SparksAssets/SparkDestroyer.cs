using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkDestroyer : MonoBehaviour
{
    [SerializeField]
    private float destructionTime = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(DestroySpark());
    }

    IEnumerator DestroySpark()
    {
        yield return new WaitForSeconds(destructionTime);
        Destroy(gameObject);
    }
}
