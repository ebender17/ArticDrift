using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBFallingPlatform : MonoBehaviour
{
    private Vector3 _startPosition;
    public float timeTilFall = 2f;
    public float timeTilDestroy = 0.5f;
    public float fallVelocityY = -10f;
    //private Vector3 fallVelocity;

    private Rigidbody _rb;

    private void Start()
    {
        _startPosition = transform.position;
        //fallVelocity = new Vector3(0f, fallVelocityY, 0f);

        _rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StartCoroutine(Fall(timeTilFall));
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision entered");
            StartCoroutine(Fall(timeTilFall));
        }
    }*/

    IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);

        _rb.isKinematic = false;
        //_rb.velocity = fallVelocity;

        Debug.Log("Platform reached target.");

        StartCoroutine(MovePlatform(timeTilDestroy, _startPosition));
    }

    IEnumerator MovePlatform(float time, Vector3 position)
    {
        yield return new WaitForSeconds(time);

        transform.rotation = Quaternion.identity;
        transform.position = position;
        _rb.isKinematic = true;
    }
}
