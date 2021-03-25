using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreCount = default;

    public void FillScorePanel(int value)
    {
        _scoreCount.SetText($"Score: {value}");
    }
}
