using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float boostAmount = 0.05f;
    [SerializeField] private float flapStrength = 100f;
    [SerializeField] private float rotateAmount = 1f;
    private AudioSource audioSource;
    [SerializeField] AudioClip flapBoostSFX;
    [SerializeField] AudioClip flapTapSFX;
    private bool hasFlapped = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    void Update() // do Input and Graphics updates here
    {
        PlayerInputFlap();
    }

    private void FixedUpdate() // do Physics engine updates here
    {
        ApplyFlap();
        PlayerInputRotate();
    }

    // GetKeyDown does something at the single frame the key is pressed at (tap)
    // GetKey does something as long as the key is pressed (hold)

    void PlayerInputFlap()
    {
        // Flap
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasFlapped = true;
            audioSource.clip = flapTapSFX;
            audioSource.Stop();
            audioSource.Play();
        }

        // Boost
        if (Input.GetKey(KeyCode.B))
        {
            rb.AddRelativeForce(Vector3.up * boostAmount * Time.deltaTime);
            audioSource.clip = flapBoostSFX;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        // Stop 
        if (Input.GetKeyUp(KeyCode.B))
        {
            audioSource.Stop();
        }
    }

    void ApplyFlap()
    {
        if (hasFlapped)
        {
            hasFlapped = false;
            rb.AddRelativeForce(Vector3.up * flapStrength * Time.deltaTime);
        }
    }

    void PlayerInputRotate()
    {
        // Rotate left
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(rotateAmount);
        }

        // Rotate right
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(-rotateAmount);
        }
    }

    private void ApplyRotation(float rotationFactor)
    {
        //rb.freezeRotation = true; // disable rigidbody rotation physics while applying manual rotation
        transform.Rotate(Vector3.forward * rotationFactor * Time.deltaTime);
        //rb.freezeRotation = false;
    }
}