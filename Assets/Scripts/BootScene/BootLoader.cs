using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class BootLoader : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        DataAPIController.instance.InitData(LoadConfigFile);
    }

    public void  LoadSceneBuffer()
    {
        Debug.Log("Load Scene Buffer");
        // T?i scene có tên "Buffer"
        //LOAD CONFIG FILE FIRST
        SceneManager.LoadScene("Buffer");
        LoadSceneManager.Instance.currrentSence = "Buffer";
        ViewManager.Instance.SwitchView(ViewIndex.LoadingView);

    }
    public void LoadConfigFile()
    {
        Debug.Log("Load Configfile");

        ConfigFileManager.Instance.Init(LoadSceneBuffer);
        

    }
}
