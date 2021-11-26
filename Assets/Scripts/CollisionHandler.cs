using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private float sceneChangeDelay = 2f;

    private AudioSource[] audioSources; // only used to grab the second AudioSource component
    private AudioSource audioSourceCollisions;
    private AudioSource audioSourceMovement;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] ParticleSystem explosionParticles;

    private bool isTransitioning = false;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        audioSourceMovement = audioSources[0];
        audioSourceCollisions = audioSources[1];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            CrashSequence();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            FinishSequence(other.gameObject);
        }
    }

    private void CrashSequence()
    {
        isTransitioning = true;
        audioSourceMovement.Stop();
        audioSourceCollisions.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false; // disable controls when crash
        explosionParticles.Play();
        Invoke("ResetScene", sceneChangeDelay);
    }

    private void FinishSequence(GameObject other)
    {
        isTransitioning = true;
        audioSourceMovement.Stop();
        audioSourceCollisions.PlayOneShot(finishSFX);
        other.GetComponentInChildren<ParticleSystem>().Play();
        Invoke("NextScene", sceneChangeDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Feather"))
        {
            PickupFeather(other);
        }
    }

    private void PickupFeather(Collider other)
    {
        // TODO - refill boost and add boost limit
        other.GetComponentInChildren<ParticleSystem>().Play();
        audioSourceCollisions.PlayOneShot(pickupSFX);
        //Destroy(other.gameObject);
    }

    private void ResetScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}