using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelLineManageHard : ScriptableObject
{
    public RythmBeatDataHard[] dataArray;

    //excel��椤���C�C�ƾڦW��
    [System.Serializable]
    public class RythmBeatDataHard
    {
        public string BeatElement;
        public string BeatPosLL;
        public string BeatPosL;
        public string BeatPosR;
        public string BeatPosRR;
    }
}
