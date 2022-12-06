using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelLineManageHard : ScriptableObject
{
    public RythmBeatDataHard[] dataArray;

    //excel表格中的每列數據名稱
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
