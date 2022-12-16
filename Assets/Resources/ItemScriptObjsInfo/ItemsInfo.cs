using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Creation/Item Units")]
[System.Serializable]
public class ItemsInfo : ScriptableObject
{
    [Tooltip("道具名稱")]
    public string itemName;
    [Tooltip("道具icon圖片")]
    public Sprite itemIcon;
    [Tooltip("該道具中獎率")]
    public float gashaProbability;
    
}
