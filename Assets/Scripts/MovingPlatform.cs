using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points;
    public int currentPoint = 0;
    private Vector3 _currentTarget;

    public float tolerance; //helps platform snap in place
    public float speed;
    public float delayTime; //delay before heading to next target
    private float delayStart;

    public bool automatic;

    private void Start()
    {
        if (points.Length > 0)
            _currentTarget = points[0];

        tolerance = speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(transform.position != _currentTarget)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    private void MovePlatform()
    {
        Vector3 heading = _currentTarget - transform.position;
        transform.position += heading.normalized * speed * Time.deltaTime;

        if (heading.magnitude < tolerance)
        {
            transform.position = _currentTarget;
            delayStart = Time.time;
        }
            
    }

    private void UpdateTarget()
    {
        if(automatic)
        {
            if(Time.time - delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        currentPoint++;
        if (currentPoint >= points.Length) currentPoint = 0;
        _currentTarget = points[currentPoint];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Entered Trigger");
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Exited Trigger");

            other.transform.parent = null;
        }
    }
    
}
