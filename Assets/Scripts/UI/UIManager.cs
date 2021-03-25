using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [Header("Score Events")]
    [SerializeField] private IntEventChannelSO _changeScoreUIEvent = default;

    private void OnEnable()
    {
        if (_changeScoreUIEvent != null)
            _changeScoreUIEvent.OnEventRaised += ChangeScoreUI;
    }

    private void OnDisable()
    {
        if (_changeScoreUIEvent != null)
            _changeScoreUIEvent.OnEventRaised -= ChangeScoreUI;
    }

    [Header("UI Panel Controllers")]
    [SerializeField]
    private UIScoreManager scoreController = default;

    public void ChangeScoreUI(int value)
    {
        scoreController.FillScorePanel(value);
    }
}
