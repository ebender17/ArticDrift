using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector3 _startPosition;
    public float fallTime = 1.5f;
    public float destroyTime = 0.5f;
    public float fallPosY = -51f;
    public float speed = 25f;

    private void Awake()
    {
        _startPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StartCoroutine(Fall(fallTime));
    }

    IEnumerator Fall(float time)
    {
        yield return new WaitForSeconds(time);

        while((transform.position.y - fallPosY) > 0.5f)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;

            yield return null; //Yields to new frame
        }

        StartCoroutine(MovePlatform(destroyTime, _startPosition));
    }

    IEnumerator MovePlatform(float time, Vector3 position)
    {
        yield return new WaitForSeconds(time);

        transform.position = position;
    }

}
