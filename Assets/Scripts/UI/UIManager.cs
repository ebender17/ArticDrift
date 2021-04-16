using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [Header("Score Event")]
    [SerializeField] private IntEventChannelSO _changeScoreUIEvent = default;

    [Header("Game Result Events")]
    [SerializeField] private GameResultChannelSO _gameResultChannel = default;

    [Header("Health Event")]
    [SerializeField] private IntEventChannelSO _changeHealthUIEvent = default;

    [Header("Dialogue Events")]
    [SerializeField] private DialogueLineChannelSO _openDialogueUIEvent = default;
    [SerializeField] private VoidEventChannelSO _closeUIDialogueEvent = default;

    [Header("Interaction Events")]
    [SerializeField] private VoidEventChannelSO _onInteractionEndedEvent = default;
    [SerializeField] private InteractionUIEventChannelSO _setInteractionEvent = default;


    private void OnEnable()
    {
        if (_changeScoreUIEvent != null)
            _changeScoreUIEvent.OnEventRaised += ChangeScoreUI;
        if (_gameResultChannel != null)
            _gameResultChannel.OnEventRaised += DisplayGameResult;
        if (_changeHealthUIEvent != null)
            _changeHealthUIEvent.OnEventRaised += ChangeHealthUI;
        if (_openDialogueUIEvent != null)
            _openDialogueUIEvent.OnEventRaised += OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised += CloseUIDialogue;
        if (_setInteractionEvent != null)
            _setInteractionEvent.OnEventRaised += SetInteractionPanel;
    }

    private void OnDisable()
    {
        if (_changeScoreUIEvent != null)
            _changeScoreUIEvent.OnEventRaised -= ChangeScoreUI;
        if (_gameResultChannel != null)
            _gameResultChannel.OnEventRaised -= DisplayGameResult;
        if (_changeHealthUIEvent != null)
            _changeHealthUIEvent.OnEventRaised -= ChangeHealthUI;
        if (_openDialogueUIEvent != null)
            _openDialogueUIEvent.OnEventRaised -= OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised -= CloseUIDialogue;
        if (_setInteractionEvent != null)
            _setInteractionEvent.OnEventRaised -= SetInteractionPanel;
    }

    [Header("UI Panel Controllers")]
    [SerializeField]
    private UIScoreManager _scoreController = default;
    [SerializeField]
    private UIGameManager _gameController = default;
    [SerializeField]
    private UIHealthManager _healthController = default;
    [SerializeField] private UIDialogueManager _dialoguePanel = default;
    [SerializeField] private UIInteractionManager _interactionPanel = default;
    [SerializeField] private GameObject _levelCompletePanel = default;

    private void ChangeScoreUI(int value)
    {
        _scoreController.FillScorePanel(value);
    }

    private void DisplayGameResult(bool isWon, int playerScore)
    {
        _gameController.FillGameResultPanel(isWon, playerScore);
        _gameController.gameObject.SetActive(true);
        _scoreController.gameObject.SetActive(false);
    }

    private void ChangeHealthUI(int value)
    {
        _healthController.FillHealthPanel(value);

        if (value <= 0)
            _healthController.gameObject.SetActive(false);
    }

    private void OpenUIDialogue(string dialogueLine, ActorSO actor)
    {
        _dialoguePanel.SetDialogue(dialogueLine, actor);
        _dialoguePanel.gameObject.SetActive(true);
    }

    private void CloseUIDialogue()
    {
        _dialoguePanel.gameObject.SetActive(false);
    }

    private void SetInteractionPanel(bool isOpen, InteractionType interactionType)
    {
        if (isOpen)
            _interactionPanel.FillInteractionPanel(interactionType);

        _interactionPanel.gameObject.SetActive(isOpen);
    }

}
