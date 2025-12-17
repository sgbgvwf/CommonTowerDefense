using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOperation
{
    public static DataOperation Instance { get; } = new DataOperation();



    /// <summary>
    /// /单个数据更新
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="oriData">原数据</param>
    /// <param name="tarData">新数据</param>
    public void UpdateSingleData<T>(ref T oriData, T newData)
    {
        oriData = newData;
    }

    public string UpdateSingleDataToString<T>(ref T oriData, T newData)
    {
        oriData = newData;
        return oriData.ToString();
    }


}
