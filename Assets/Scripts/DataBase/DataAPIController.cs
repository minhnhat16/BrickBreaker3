
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataAPIController : MonoBehaviour
{
    public static DataAPIController instance;

    [SerializeField]
    private DataModel dataModel;

    private void Awake()
    {
        instance = this;
    }

    public void InitData(Action callback)
    {
        Debug.Log("(BOOT) // INIT DATA");

        dataModel.InitData(() =>
        {
           // CheckDailyLogin();
            callback();
        });

        Debug.Log("==========> BOOT PROCESS SUCCESS <==========");
    }

    #region Get Data
    #endregion


    #region Others


    #endregion
}
