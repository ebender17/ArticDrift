using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Respawn trigger entered!");
        GameController gc = other.gameObject.GetComponent<GameController>();
        
        if (gc != null)
        {
            gc.PlayerFallRespawn();
        }
    }
}
