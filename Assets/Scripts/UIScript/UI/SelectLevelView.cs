using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectLevelView : BaseView
{
    public GameObject ScrollView;
    private void OnEnable()
    {
     
    }
    public void SpawnLevel()
    {
        ScrollView.GetComponentInChildren<LevelManager>().SpawnLevel();
    }
}
