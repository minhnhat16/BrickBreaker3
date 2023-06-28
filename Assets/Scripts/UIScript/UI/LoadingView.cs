using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingView : BaseView
{
    public void Start()
    {
        StartCoroutine(LoadViewAfterDelay(0f));

    }
    IEnumerator LoadViewAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ViewManager.Instance.SwitchView(ViewIndex.MainScreenView, null, null);
    }
}
