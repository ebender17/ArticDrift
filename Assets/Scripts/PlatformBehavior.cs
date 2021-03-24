using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;

    private Rigidbody rb = null;
    private Vector3 currentPos;

    PlayerController cc; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position, Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
        rb.MovePosition(currentPos);

        if (rb != null && cc != null)
            cc.externalMovement = rb.velocity * Time.deltaTime;
            //cc.Move(rb.velocity * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            cc = other.GetComponent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            cc.externalMovement = Vector3.zero;
            cc = null;
    }
}
