using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private float sceneChangeDelay = 2f;
    
    private AudioSource[] audioSources;
    private AudioSource audioSourceCollisions;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip pickupSFX;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        audioSourceCollisions = audioSources[1];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            CrashSequence();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            FinishSequence();
        }
    }

    private void CrashSequence()
    {
        audioSourceCollisions.PlayOneShot(crashSFX);
        // TODO - add particle vfx
        GetComponent<Movement>().enabled = false;
        Invoke("ResetScene", sceneChangeDelay);
    }

    private void FinishSequence()
    {
        audioSourceCollisions.PlayOneShot(finishSFX);
        // TODO - add particle vfx
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
        // TODO - refill boost
        // TODO - feather vfx
        audioSourceCollisions.PlayOneShot(pickupSFX);
        Destroy(other.gameObject);
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