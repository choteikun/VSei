using System.IO;
using System.Data;
using UnityEngine;
using Excel;
using static ExcelLineManageNormal;
using static ExcelLineManageHard;

public class ExcelTool
{
    public static RythmBeatDataNormal[] CreatNormalBeatArrayWithExcel(string filePath)
    {
        //行與列
        int columnNum = 0, rowNum = 0;
        DataRowCollection collection = ReadExcel(filePath, ref columnNum, ref rowNum);//獲得行與列的值
        //從第二行開始才是有效數據
        RythmBeatDataNormal[] array = new RythmBeatDataNormal[rowNum - 1];
        for (int i = 1; i < rowNum; i++)
        {
            RythmBeatDataNormal rythmBeatData = new RythmBeatDataNormal
            {
                BeatElement = collection[i][0].ToString(),
                BeatPosLL = collection[i][1].ToString(),
                BeatPosL = collection[i][2].ToString(),
                BeatPosR = collection[i][3].ToString(),
                BeatPosRR = collection[i][4].ToString(),
            };
            array[i - 1] = rythmBeatData;
        }
        return array;
    }
    public static RythmBeatDataHard[] CreatHardBeatArrayWithExcel(string filePath)
    {
        //行與列
        int columnNum = 0, rowNum = 0;
        DataRowCollection collection = ReadExcel(filePath, ref columnNum, ref rowNum);//獲得行與列的值
        //從第二行開始才是有效數據
        RythmBeatDataHard[] array = new RythmBeatDataHard[rowNum - 1];
        for (int i = 1; i < rowNum; i++)
        {
            RythmBeatDataHard rythmBeatData = new RythmBeatDataHard
            {
                BeatElement = collection[i][0].ToString(),
                BeatPosLL = collection[i][1].ToString(),
                BeatPosL = collection[i][2].ToString(),
                BeatPosR = collection[i][3].ToString(),
                BeatPosRR = collection[i][4].ToString(),
            };
            array[i - 1] = rythmBeatData;
        }
        return array;
    }
    /// <summary>
    /// 讀取excel文件內容
    /// </summary>
    /// <param name="filePath">文件路徑</param>
    /// <param name="columnnum">行數</param>
    /// <param name="rownum">列數</param>
    /// <returns></returns>
    static DataRowCollection ReadExcel(string filePath, ref int columnnum, ref int rownum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下標0表示excel文件中第一張表的數據
        columnnum = result.Tables[0].Columns.Count;
        rownum = result.Tables[0].Rows.Count;
        stream.Close();
        return result.Tables[0].Rows;
    }
}
