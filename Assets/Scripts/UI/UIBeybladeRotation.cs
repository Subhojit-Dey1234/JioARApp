using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBeybladeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.0f, 1000f)]
    public float initialSpeed = 20f;
    [SerializeField]
    private float retardRate = 10f;
    [SerializeField]
    private float soundDecayRate = 10f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float timeDelay = 10f;
    [SerializeField]
    private GameObject model;
    private float speed;
    private float time = 0;
    private bool startRotating = false;
    private bool stopRotating = false;

    private void Start()
    {
        speed = 0;
        model.SetActive(false);
        
    }
    private void OnEnable()
    {
        UIAnimationManager.OnTextAppeared += StarAnimating;
        UIAnimationManager.OnBeyBladeAppeared += StartRotation;
    }
    private void OnDisable()
    {
        UIAnimationManager.OnTextAppeared -= StarAnimating;
        UIAnimationManager.OnBeyBladeAppeared -= StartRotation;
        
    }
    public void StarAnimating()
    {
        StartCoroutine(StartAnimation());
    }


    public void StartRotation()
    {
        speed = initialSpeed;
        time = 0;
        AudioManager.Instance.PlaySoundWithDecay("BeyBladeSpin", soundDecayRate);
        startRotating = true;
    }
    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(timeDelay);
        animator.SetBool("ShouldAnimate", true);
        model.SetActive(true);
    }
    void Update()
    {
        if (!startRotating)
            return;
        time++;
        Retard();
        transform.RotateAround(transform.position, transform.up, speed);
        // rb.AddTorque(transform.up * torque * speed);
    }

    private void Retard()
    {
        if (stopRotating)
        {
            return;
        }
        if (speed <= 0.001f * initialSpeed)
        {
            GameManager.Instance.LoadInstructions();
            stopRotating = true;
        }
        speed = initialSpeed * Mathf.Exp(-1 * time * retardRate);
        
    }
}
