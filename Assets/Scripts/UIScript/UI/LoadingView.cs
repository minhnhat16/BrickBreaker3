using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingView : BaseView
{
    public void Start()
    {
        StartCoroutine(LoadViewAfterDelay(0.2f));

    }
    IEnumerator LoadViewAfterDelay(float delay)
    {
        Debug.Log("Load MainScreenView");
        yield return new WaitForSeconds(delay);
        ViewManager.Instance.SwitchView(ViewIndex.MainScreenView, null, null);
    }
}
