using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelLineManageNormal : ScriptableObject
{
    public RythmBeatDataNormal[] dataArray;

    //excel��椤���C�C�ƾڦW��
    [System.Serializable]
    public class RythmBeatDataNormal
    {
        public string BeatElement;
        public string BeatPosLL;
        public string BeatPosL;
        public string BeatPosR;
        public string BeatPosRR;
    }
}
