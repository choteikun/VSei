using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyAccount : ScriptableObject
{

    [Tooltip("���a�ҳЫت��W�r")]
    public string myAccountName;

    [Tooltip("��O��")]
    public int stamina;

    [Tooltip("�O�_�����Ẹ���ۡE������")]
    public bool FelbelemAlice;
    [Tooltip("�O�_������d�E���̦�")]
    public bool AikaAmimi;
    [Tooltip("�O�_������������E�ڭ�")]
    public bool MalibetaRorem;
    [Tooltip("�O�_�����L�W")]
    public bool Nameless;
    [Tooltip("�O�_�����զ�.�Ȧ�")]
    public bool ShiorhaiYai;

    [Tooltip("�O�_���b�ϥΤ��ƥ[���D��")]
    public bool PointBounsItemUsing = false;
    [Tooltip("�O�_���b�ϥΦ�q�[���D��")]
    public bool HpAddItemUsing = false;

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
    public enum CurCharacterUse
    {
        FelbelemAlice,
        AikaAmimi,
        MalibetaRorem,
        Nameless,
        ShiorhaiYai
    }
    public CurCharacterUse curCharacterUse;
}
