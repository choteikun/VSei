using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelLineManageNormal : ScriptableObject
{
    public RythmBeatDataNormal[] dataArray;

    //excel表格中的每列數據名稱
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
