using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [Header("Score Event")]
    [SerializeField] private IntEventChannelSO _changeScoreUIEvent = default;

    [Header("Game Result Events")]
    [SerializeField] private GameResultChannelSO _gameResultChannelSO = default;

    [Header("Health Event")]
    [SerializeField] private IntEventChannelSO _changeHealthUIEvent = default;

    private void OnEnable()
    {
        if (_changeScoreUIEvent != null)
            _changeScoreUIEvent.OnEventRaised += ChangeScoreUI;
        if (_gameResultChannelSO != null)
            _gameResultChannelSO.OnEventRaised += DisplayGameResult;
        if (_changeHealthUIEvent != null)
            _changeHealthUIEvent.OnEventRaised += ChangeHealthUI;
    }

    private void OnDisable()
    {
        if (_changeScoreUIEvent != null)
            _changeScoreUIEvent.OnEventRaised -= ChangeScoreUI;
        if (_gameResultChannelSO != null)
            _gameResultChannelSO.OnEventRaised -= DisplayGameResult;
        if (_changeHealthUIEvent != null)
            _changeHealthUIEvent.OnEventRaised -= ChangeHealthUI;
    }

    [Header("UI Panel Controllers")]
    [SerializeField]
    private UIScoreManager _scoreController = default;
    [SerializeField]
    private UIGameManager _gameController = default;
    [SerializeField]
    private UIHealthManager _healthController = default;

    public void ChangeScoreUI(int value)
    {
        _scoreController.FillScorePanel(value);
    }

    public void DisplayGameResult(bool isWon, int playerScore)
    {
        _gameController.FillGameResultPanel(isWon, playerScore);
        _gameController.gameObject.SetActive(true);
        _scoreController.gameObject.SetActive(false);
    }

    public void ChangeHealthUI(int value)
    {
        _healthController.FillHealthPanel(value); 
    }
}
