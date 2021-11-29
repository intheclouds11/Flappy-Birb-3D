using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    private float movementFactor;
    [SerializeField] private float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period; // continually growing over time
        const float tau = 2 * Mathf.PI; // total radians in a circle
        float rawSinWave = Mathf.Sin(cycles * tau); // oscillate from -1 to 1
        
        //movementFactor = (rawSinWave + 1) / 2; // recalc to go from 0 to 1, cleaner looking
        movementFactor = rawSinWave;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}