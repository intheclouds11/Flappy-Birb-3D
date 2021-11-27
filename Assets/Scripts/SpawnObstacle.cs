using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void Update()
    {
        Invoke("Spawn", delayTime);
    }

    private void Spawn()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }
 
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}