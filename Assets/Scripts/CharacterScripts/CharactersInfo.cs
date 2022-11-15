using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Creation/Character Units")]
public class CharactersInfo : ScriptableObject
{
    public string charName;
    public string charType;
    public int charLevel;
}
