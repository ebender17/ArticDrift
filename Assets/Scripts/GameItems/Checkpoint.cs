using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController gc = other.gameObject.GetComponent<GameController>();
            if (gc != null)
            {
                gc.CurrentCheckpoint = transform.position;

                material.color = new Color(0.3372549f, 0.97254902f, 0.85490196f);

                Destroy(this);
                
            }
        }
    }
}
