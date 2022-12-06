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
        //��P�C
        int columnNum = 0, rowNum = 0;
        DataRowCollection collection = ReadExcel(filePath, ref columnNum, ref rowNum);//��o��P�C����
        //�q�ĤG��}�l�~�O���ļƾ�
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
        //��P�C
        int columnNum = 0, rowNum = 0;
        DataRowCollection collection = ReadExcel(filePath, ref columnNum, ref rowNum);//��o��P�C����
        //�q�ĤG��}�l�~�O���ļƾ�
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
    /// Ū��excel��󤺮e
    /// </summary>
    /// <param name="filePath">�����|</param>
    /// <param name="columnnum">���</param>
    /// <param name="rownum">�C��</param>
    /// <returns></returns>
    static DataRowCollection ReadExcel(string filePath, ref int columnnum, ref int rownum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        //Tables[0] �U��0���excel��󤤲Ĥ@�i���ƾ�
        columnnum = result.Tables[0].Columns.Count;
        rownum = result.Tables[0].Rows.Count;
        stream.Close();
        return result.Tables[0].Rows;
    }
}
