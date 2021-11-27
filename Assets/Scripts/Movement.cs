using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float boostAmount = 0.05f;
    [SerializeField] private float flapStrength = 100f;
    [SerializeField] private float rotateAmount = 1f;

    private AudioSource[] audioSources; // only used to grab the first AudioSource component
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
        PlayerInputForce();
    }

    private void FixedUpdate() // do Physics engine updates here
    {
        PlayerInputRotate();
        ApplyFlapForce();
    }

    void PlayerInputForce()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasFlapped = true; // triggers ApplyFlapForce()
            FlapAudio();
        }

        if (Input.GetKey(KeyCode.B))
        {
            ApplyBoostForce();
        }

        // Stop boost audio
        if (Input.GetKeyUp(KeyCode.B))
        {
            audioSourceMovement.Stop();
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

    void ApplyFlapForce()
    {
        if (hasFlapped)
        {
            hasFlapped = false;
            rb.AddRelativeForce(Vector3.up * flapStrength * Time.deltaTime);
        }
    }

    void ApplyBoostForce()
    {
        rb.AddRelativeForce(Vector3.up * boostAmount * Time.deltaTime);
        audioSourceMovement.clip = flapBoostSFX;
        flapBoostParticles.Play();

        if (!audioSourceMovement.isPlaying)
        {
            audioSourceMovement.Play();
        }
    }

    void FlapAudio()
    {
        audioSourceMovement.clip = flapTapSFX;
        audioSourceMovement.Stop();
        audioSourceMovement.Play();
    }

    private void ApplyRotation(float rotationFactor)
    {
        //rb.freezeRotation = true; // disable rigidbody rotation physics while applying manual rotation
        transform.Rotate(Vector3.forward * rotationFactor * Time.deltaTime);
        //rb.freezeRotation = false;
    }
}