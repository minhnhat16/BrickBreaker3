using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfirmDialog : BaseDialog
{
    [SerializeField] private Text TextLevel;

    private void Start()
    {
       TextLevel =  GameObject.Find("Level_Text").GetComponent<Text>();
    }
}
