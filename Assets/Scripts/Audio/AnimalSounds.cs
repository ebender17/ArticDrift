using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAnimalSounds", menuName = "Audio/Animal Sounds")]
public class AnimalSounds : ScriptableObject
{
    public Sound idle;
    public Sound walking;
    public Sound eating;
    
}
