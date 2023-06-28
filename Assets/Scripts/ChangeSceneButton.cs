using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public LevelManager levelManage;

    public void ChangeLeveleListView()
    {
        levelManage.gameObject.SetActive(true);
        levelManage.SpawnLevel();
    }
}

