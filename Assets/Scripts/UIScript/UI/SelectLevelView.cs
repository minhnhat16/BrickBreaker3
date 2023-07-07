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
        ScrollView.GetComponentInChildren<LevelManager>().SpawnLevel();
      
    }
    public void checkHighestLevel()
    {
       
    }
}
