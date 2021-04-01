using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint trigger entered!");
        GameController player = other.gameObject.GetComponent<GameController>();
        if(player != null)
        {
            player.CurrentCheckpoint = transform.position;
        }

    }
}
