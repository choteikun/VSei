using System.IO;
using UnityEditor;
using UnityEngine;

public class CreatAssetWithExcel : Editor
{
    [MenuItem("Tools/�ͦ�asset���")]
    private static void CreatExcel()
    {
        ExcelLineManageNormal normalManager = ScriptableObject.CreateInstance<ExcelLineManageNormal>();
        ExcelLineManageHard hardManager = ScriptableObject.CreateInstance<ExcelLineManageHard>();
        //���
        normalManager.dataArray = ExcelTool.CreatNormalBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatNormalSet_StreetCityPop.xlsx");
        //normalManager.dataArray = ExcelTool.CreatNormalBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatNormalSet_Take_Off_Into_the_Sky.xlsx");

        hardManager.dataArray = ExcelTool.CreatHardBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatHardSet_StreetCityPop.xlsx");
        //hardManager.dataArray = ExcelTool.CreatHardBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatHardSet_Take_Off_Into_the_Sky.xlsx");
        //�T�O��󧨦s�b
        if (!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }
        string assetPathNormalBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatNormalSet_StreetCityPop");
        //string assetPathNormalBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatNormalSet_Take_Off_Into_the_Sky");

        string assetPathHardBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatHardSet_StreetCityPop");
        //string assetPathHardBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatHardSet_Take_Off_Into_the_Sky");
        //�ͦ�Asset���
        AssetDatabase.CreateAsset(normalManager, assetPathNormalBeat);
        AssetDatabase.CreateAsset(hardManager, assetPathHardBeat);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
