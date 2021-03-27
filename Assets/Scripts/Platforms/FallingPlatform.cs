using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector3 _startPosition;
    public float fallTime = 2f;
    public float destroyTime = 0.5f;
    public float fallPosY = -50f;
    public float smoothing = 1f;

    private void Awake()
    {
        _startPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StartCoroutine(Fall(fallTime));
    }
    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Collision entered");
                StartCoroutine(Fall(fallTime));
            }
        }
    }

    IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 fallPosition = new Vector3(transform.position.x, fallPosY, transform.position.z);
        while((transform.position.y - fallPosY) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, fallPosition, smoothing * Time.deltaTime);

            yield return null; //Yields to new frame
        }

        Debug.Log("Platform reached target.");

        StartCoroutine(MovePlatform(destroyTime, _startPosition));
    }

    IEnumerator MovePlatform(float time, Vector3 position)
    {
        yield return new WaitForSeconds(time);

        transform.position = position;
    }

}
