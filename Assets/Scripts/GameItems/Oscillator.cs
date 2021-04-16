using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 _startingPosition;
    [SerializeField] private Vector3 _movementVector;
    private float _movementFactor;
    [SerializeField] private float _period = 2f;


    const float TAU = Mathf.PI * 2;
    
    // Start is called before the first frame update
    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (_period <= Mathf.Epsilon) { return; } //compare floating point to Mathf.Epsilon instead of 0 due to floating point precision

        float cycles = Time.time / _period; //continually grows over time

        float rawSinWave = Mathf.Sin(cycles * TAU); //Will give us a value between -1 and 1

        _movementFactor = (rawSinWave + 1f) / 2f; //transform to 0 and 2 then divide by 2 to get value between 0 and 1

        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }

}
