using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAccount : ScriptableObject
{

    [Tooltip("���a�ҳЫت��W�r")]
    public string myAccountName;

    [Tooltip("��O��")]
    public float stamina;

    [Tooltip("��d�E���̦�")]
    public bool AikaAmimi;
    [Tooltip("�Ẹ���ۡE������")]
    public bool FelbelemAlice;
    [Tooltip("��������E�ڭ�")]
    public bool MalibetaRorem;
    [Tooltip("�L�W")]
    public bool Nameless;
    [Tooltip("�զ�.�Ȧ�")]
    public bool ShiorhaiYai;

    [Tooltip("�оǫ�������")]
    public bool tutorialClose;

    [Tooltip("�Ĥ@���i�J�C��")]
    public bool firstPlay;

    [Tooltip("��q����")]
    public int EnergyDrink;
    [Tooltip("����H��")]
    public int CharacterFragment;
    [Tooltip("���ƥ[���D��")]
    public int PointBounsItem;
    [Tooltip("��q�[���D��")]
    public int HpAddItem;
    [Tooltip("�U��")]
    public int Trash;
}
