using System.IO;
using UnityEditor;
using UnityEngine;

public class CreatAssetWithExcel : Editor
{
    [MenuItem("Tools/生成asset文件")]
    private static void CreatExcel()
    {
        ExcelLineManage manager = ScriptableObject.CreateInstance<ExcelLineManage>();
        //賦值
        manager.dataArray = ExcelTool.CreatItemArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatSet_StreetCityPop.xlsx");
        //manager.dataArray = ExcelTool.CreatItemArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatSet_Take_Off_Into_the_Sky.xlsx");
        //確保文件夾存在
        if (!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }
        string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatSet_StreetCityPop");
        //string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatSet_Take_Off_Into_the_Sky");
        //生成一個Asset文件
        AssetDatabase.CreateAsset(manager, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
