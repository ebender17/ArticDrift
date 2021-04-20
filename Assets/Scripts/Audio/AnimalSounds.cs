using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAnimalSounds", menuName = "Audio/Animal Sounds")]
public class AnimalSounds : ScriptableObject
{
    public Sound idle;
    public Sound fishPickup;
    public Sound iceJump;
    public Sound iceLand;
    public Sound snowJump;
    public Sound snowLand;
    public Sound[] snowFootsteps;
    public Sound[] iceFootsteps;
    
}
