using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextTemp : MonoBehaviour
{
    public void PopUpPlayKick()
    {
        GameManager.Instance.LoadOnInGameController();
        LoadLevel.instance.LevelSelect(this.gameObject.GetComponent<Text>().text);
       LoadLevel.instance.currentLevelLoad = Convert.ToInt32(this.gameObject.GetComponent<Text>().text);
    }
}
