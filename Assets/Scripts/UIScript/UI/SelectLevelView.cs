using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectLevelView : BaseView
{
    public GameObject ScrollView;
    public override void OnStartShowView()
    {
        //Debug.Log("Onstart show select level view");
        ScrollView.GetComponentInChildren<LevelManager>().SpawnLevel();
        ScrollView.GetComponentInChildren<LevelManager>().HighestLevelOn();
    }
    public override void OnEndHideView()
    {
        ScrollView.GetComponentInChildren<LevelManager>().HighestLevelOff();
    }
    public void checkHighestLevel()
    {
       
    }
}
