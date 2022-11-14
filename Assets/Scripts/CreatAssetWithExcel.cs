using System.IO;
using UnityEditor;
using UnityEngine;

public class CreatAssetWithExcel : Editor
{
    [MenuItem("Tools/�ͦ�asset���")]
    private static void CreatExcel()
    {
        ExcelLineManage manager = ScriptableObject.CreateInstance<ExcelLineManage>();
        //���
        manager.dataArray = ExcelTool.CreatItemArrayWithExcel(ExcelConfig.excelsFolderPath + "rythmBeatSetTest.xlsx");
        //�T�O��󧨦s�b
        if (!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }

        string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "rythmBeatSetTest");
        //�ͦ��@��Asset���
        AssetDatabase.CreateAsset(manager, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
