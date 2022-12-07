using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SongsInfo", menuName = "SongNumberData")]
[System.Serializable]
public class SongsInfo : ScriptableObject
{
    public int songNumIs;
    public int trackID;
    public string difficultySelection;
}
