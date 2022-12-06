using System.IO;
using UnityEditor;
using UnityEngine;

public class CreatAssetWithExcel : Editor
{
    [MenuItem("Tools/生成asset文件")]
    private static void CreatExcel()
    {
        ExcelLineManageNormal normalManager = ScriptableObject.CreateInstance<ExcelLineManageNormal>();
        ExcelLineManageHard hardManager = ScriptableObject.CreateInstance<ExcelLineManageHard>();
        //賦值
        normalManager.dataArray = ExcelTool.CreatNormalBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatNormalSet_StreetCityPop.xlsx");
        //normalManager.dataArray = ExcelTool.CreatNormalBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatNormalSet_Take_Off_Into_the_Sky.xlsx");

        hardManager.dataArray = ExcelTool.CreatHardBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatHardSet_StreetCityPop.xlsx");
        //hardManager.dataArray = ExcelTool.CreatHardBeatArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatHardSet_Take_Off_Into_the_Sky.xlsx");
        //確保文件夾存在
        if (!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }
        string assetPathNormalBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatNormalSet_StreetCityPop");
        //string assetPathNormalBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatNormalSet_Take_Off_Into_the_Sky");

        string assetPathHardBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatHardSet_StreetCityPop");
        //string assetPathHardBeat = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatHardSet_Take_Off_Into_the_Sky");
        //生成Asset文件
        AssetDatabase.CreateAsset(normalManager, assetPathNormalBeat);
        AssetDatabase.CreateAsset(hardManager, assetPathHardBeat);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
