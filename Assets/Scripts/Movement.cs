using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float boostAmount = 0.05f;
    [SerializeField] private float flapStrength = 100f;
    [SerializeField] private float rotateAmount = 1f;

    private AudioSource[] audioSources;  // only used to grab the first AudioSource component
    private AudioSource audioSourceMovement;
    private AudioSource audioSourceCollisions;
    [SerializeField] AudioClip flapTapSFX;
    [SerializeField] private AudioClip flapBoostSFX;
    [SerializeField] private ParticleSystem flapBoostParticles;
    private Rigidbody rb;

    private bool hasFlapped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
        audioSourceMovement = audioSources[0];
        audioSourceCollisions = audioSources[1];
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

    // GetKeyDown does something at the single frame the key is pressed at. ("tap")
    // GetKey does something as long as the key is pressed. ("hold")

    void PlayerInputFlap()
    {
        // Flap
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasFlapped = true;
            audioSourceMovement.clip = flapTapSFX;
            audioSourceMovement.Stop();
            audioSourceMovement.Play();
        }

        // Boost
        if (Input.GetKey(KeyCode.B))
        {
            rb.AddRelativeForce(Vector3.up * boostAmount * Time.deltaTime);
            audioSourceMovement.clip = flapBoostSFX;
            flapBoostParticles.Play();

            if (!audioSourceMovement.isPlaying)
            {
                audioSourceMovement.Play();
            }
        }

        // Stop 
        if (Input.GetKeyUp(KeyCode.B))
        {
            audioSourceMovement.Stop();
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