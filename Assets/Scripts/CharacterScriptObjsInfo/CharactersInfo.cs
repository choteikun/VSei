using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Creation/Character Units")]
public class CharactersInfo : ScriptableObject
{
    [Tooltip("角色名稱")]
    public string charName;
    [Tooltip("角色屬性")]
    public string charType;
    [Tooltip("角色等級")]
    public int charLevel;
    [Tooltip("角色FeverTime")]
    public float charFeverTime;
    [Tooltip("角色Hp")]
    public int charHp;
    [Tooltip("該角色中獎率")]
    public float gashaProbability;
}
