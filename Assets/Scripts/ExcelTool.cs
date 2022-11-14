using System.IO;
using System.Data;
using UnityEngine;
using Excel;
using static ExcelLineManage;

public class ExcelTool
{
    public static RythmBeatData[] CreatItemArrayWithExcel(string filePath)
    {
        //��P�C
        int columnNum = 0, rowNum = 0;
        DataRowCollection collection = ReadExcel(filePath, ref columnNum, ref rowNum);//��o��P�C����
        //�q�ĤG��}�l�~�O���ļƾ�
        RythmBeatData[] array = new RythmBeatData[rowNum - 1];
        for (int i = 1; i < rowNum; i++)
        {
            RythmBeatData rythmBeatData = new RythmBeatData
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
