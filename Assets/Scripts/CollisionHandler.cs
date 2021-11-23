using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Invoke("ResetLevel", 2f);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Invoke("ResetLevel", 2f);
            // particles
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Feather"))
        {
            Destroy(other.gameObject);
        }
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
