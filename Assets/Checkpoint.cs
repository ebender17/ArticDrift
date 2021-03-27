using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint trigger entered!");
        GameLogic player = other.gameObject.GetComponent<GameLogic>();
        if(player != null)
        {
            player.CurrentCheckpoint = transform.position;
        }

    }
}
