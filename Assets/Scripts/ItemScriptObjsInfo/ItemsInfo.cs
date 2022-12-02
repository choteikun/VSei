using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Creation/Item Units")]
public class ItemsInfo : ScriptableObject
{
    [Tooltip("道具名稱")]
    public string itemName;
    [Tooltip("該道具中獎率")]
    public float gashaProbability;
}
