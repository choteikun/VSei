using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Creation/Item Units")]
[System.Serializable]
public class ItemsInfo : ScriptableObject
{
    [Tooltip("�D��W��")]
    public string itemName;
    [Tooltip("�D��icon�Ϥ�")]
    public Sprite itemIcon;
    [Tooltip("�ӹD�㤤���v")]
    public float gashaProbability;
    
}
