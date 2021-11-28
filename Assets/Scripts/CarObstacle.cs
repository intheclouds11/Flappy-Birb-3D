using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    [SerializeField] float obstacleSpeedX = 0;
    [SerializeField] float obstacleSpeedY = 0;
    [SerializeField] float obstacleSpeedZ = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(obstacleSpeedX, obstacleSpeedY, obstacleSpeedZ);
    }
}