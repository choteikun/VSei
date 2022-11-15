using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelLineManage : ScriptableObject
{
    public RythmBeatData[] dataArray;

    //excel��椤���C�C�ƾڦW��
    [System.Serializable]
    public class RythmBeatData
    {
        public string BeatElement;
        public string BeatPosLL;
        public string BeatPosL;
        public string BeatPosR;
        public string BeatPosRR;
    }
}
