using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] float cloudXMoveSpeed = 0;
    [SerializeField] float cloudYMoveSpeed = 0;
    [SerializeField] float cloudZMoveSpeed = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(cloudXMoveSpeed, cloudYMoveSpeed, cloudZMoveSpeed);
    }
}
