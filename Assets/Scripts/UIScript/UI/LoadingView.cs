using System.Collections;
using UnityEngine;

public class LoadingView : BaseView
{
    public override void OnStartShowView()
    {
        Debug.Log("ON START SHOW LOADING VIEW");
        if (LoadSceneManager.Instance.currrentSence == "Buffer")
        {
            StartCoroutine(LoadViewAfterDelay(0.3f, ViewIndex.SelectLevelView));
        }
        else if (LoadSceneManager.Instance.currrentSence == "Ingame") {
            StartCoroutine(LoadViewAfterDelay(1f, ViewIndex.GameplayView));
        }
    }
    IEnumerator LoadViewAfterDelay(float delay, ViewIndex index)
    {
        Debug.Log("LOAD VIEW AFTER DELAY");
        yield return new WaitForSeconds(delay);
        ViewManager.Instance.SwitchView(index, null, null);
    }
}
