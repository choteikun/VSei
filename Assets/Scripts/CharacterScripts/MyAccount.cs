using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAccount : ScriptableObject
{
    [Tooltip("玩家所創建的名字")]
    public string myAccountName;

    [Tooltip("體力值")]
    public int stamina;

    [Tooltip("艾卡‧阿米米")]
    public bool aikaAmimi;
    [Tooltip("菲爾貝倫‧阿莉絲")]
    public bool felbelemAlice;
    [Tooltip("瑪莉貝塔‧蘿倫")]
    public bool malibetaRorem;
    [Tooltip("無名")]
    public bool nameless;
    [Tooltip("白灰.亞衣")]
    public bool shiorhaiYai;

}
