using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHealthManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _health = default;

    public void FillHealthPanel(int value)
    {
        _health.SetText($"Health: {value}");
    } 
}
