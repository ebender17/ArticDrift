using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int collectableCount = 0;

    [Header("Broadcasting on")]
    [SerializeField] private IntEventChannelSO _changeScoreUIEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            collectableCount++;
            _changeScoreUIEvent.RaiseEvent(collectableCount);

            Destroy(other.gameObject);
        }
    }

}
