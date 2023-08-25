using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingView : BaseView
{
    public override void OnStartShowView()
    {
        Debug.Log("ON START SHOW LOADING VIEW");
        StartCoroutine(LoadViewAfterDelay(0.3f));
        
    }
    IEnumerator LoadViewAfterDelay(float delay)
    {
        Debug.Log("LOAD VIEW AFTER DELAY"); 
        yield return new WaitForSeconds(delay);
        ViewManager.Instance.SwitchView(ViewIndex.MainScreenView, null, null);
    }
}
