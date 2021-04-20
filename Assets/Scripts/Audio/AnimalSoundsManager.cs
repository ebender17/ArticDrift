using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to animal and trigger methods using animation events.
/// </summary>
public class AnimalSoundsManager : MonoBehaviour
{
    private PlayerController _playerController;
    [Header("Broadcasting on")]
    [SerializeField] private AudioSoundEventChannelSO _playSFXChannel;
    [SerializeField] private AudioSoundsEventChannelSO _playerSFXRandomChannel;

    [SerializeField] private AnimalSounds _animalSounds;

    private bool _isPlayerOnIce;
    private bool _isPlayerGrounded;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }


    public void PlayIdle()
    {
        _playSFXChannel.RaiseEvent(_animalSounds.idle, transform.position);
    }

    public void PlayFishPickup()
    {
        _playSFXChannel.RaiseEvent(_animalSounds.fishPickup, transform.position);
    }

    public void PlayFootstep()
    {
        Debug.Log("Play Snowfootstep called.");

        _isPlayerOnIce = _playerController.IsOnIce;
        _isPlayerGrounded = _playerController.IsGrounded;
        
        if(!_isPlayerOnIce && _isPlayerGrounded)
            _playerSFXRandomChannel.RaiseEvent(_animalSounds.snowFootsteps, transform.position);
        else if(_playerController.IsOnIce && _isPlayerGrounded)
            _playerSFXRandomChannel.RaiseEvent(_animalSounds.iceFootsteps, transform.position);
    }

    public void PlayJump()
    {
        _isPlayerOnIce = _playerController.IsOnIce;
        _isPlayerGrounded = _playerController.IsGrounded;

        if (!_isPlayerOnIce)
            _playSFXChannel.RaiseEvent(_animalSounds.snowJump, transform.position);
        else if (_isPlayerOnIce)
            _playSFXChannel.RaiseEvent(_animalSounds.iceJump, transform.position);
    }

    public void PlayLand()
    {
        _isPlayerOnIce = _playerController.IsOnIce;
        _isPlayerGrounded = _playerController.IsGrounded;

        if (!_isPlayerOnIce)
            _playSFXChannel.RaiseEvent(_animalSounds.snowLand, transform.position);
        else if (_isPlayerOnIce)
            _playSFXChannel.RaiseEvent(_animalSounds.iceLand, transform.position);
    }

}
