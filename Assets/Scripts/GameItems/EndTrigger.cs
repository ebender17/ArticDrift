using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameController gc = other.gameObject.GetComponent<GameController>();
            if(gc != null)
            {
                gc.LevelComplete();
            }
        }
    }
}
