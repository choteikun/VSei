using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Creation/Item Units")]
public class ItemsInfo : ScriptableObject
{
    [Tooltip("�D��W��")]
    public string itemName;
    [Tooltip("�ӹD�㤤���v")]
    public float gashaProbability;
}
