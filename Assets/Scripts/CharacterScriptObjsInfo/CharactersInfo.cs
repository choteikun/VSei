using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Creation/Character Units")]
public class CharactersInfo : ScriptableObject
{
    [Tooltip("����W��")]
    public string charName;
    [Tooltip("�����ݩ�")]
    public string charType;
    [Tooltip("���ⵥ��")]
    public int charLevel;
    [Tooltip("����FeverTime")]
    public float charFeverTime;
    [Tooltip("����Hp")]
    public int charHp;
    [Tooltip("�Ө��⤤���v")]
    public float gashaProbability;
}
