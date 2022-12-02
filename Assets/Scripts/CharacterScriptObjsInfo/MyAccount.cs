using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAccount : ScriptableObject
{

    [Tooltip("玩家所創建的名字")]
    public string myAccountName;

    [Tooltip("體力值")]
    public float stamina;

    [Tooltip("艾卡•阿米米")]
    public bool AikaAmimi;
    [Tooltip("菲爾貝倫•阿莉絲")]
    public bool FelbelemAlice;
    [Tooltip("瑪莉貝塔•蘿倫")]
    public bool MalibetaRorem;
    [Tooltip("無名")]
    public bool Nameless;
    [Tooltip("白灰.亞衣")]
    public bool ShiorhaiYai;

    [Tooltip("教學指引關閉")]
    public bool tutorialClose;

    [Tooltip("第一次進入遊戲")]
    public bool firstPlay;

    [Tooltip("能量飲料")]
    public int EnergyDrink;
    [Tooltip("角色碎片")]
    public int CharacterFragment;
    [Tooltip("分數加成道具")]
    public int PointBounsItem;
    [Tooltip("血量加成道具")]
    public int HpAddItem;
    [Tooltip("垃圾")]
    public int Trash;
}
