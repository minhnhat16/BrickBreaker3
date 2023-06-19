using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public GameObject levelist;
    public void ChangeScene(string sceneName)
    {
        GameManager.Instance.LoadOnInGameController();
    }
    public void ChangeLeveleListView()
    {
        levelist.SetActive(true);
        gameObject.SetActive(false);
    }
}

