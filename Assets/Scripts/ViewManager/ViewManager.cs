using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;
    public Transform anchorView;
    public Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();
    public BaseView currentView = null;

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (ViewIndex viewIndex in ViewConfig.viewArray)
        {
            string viewName = viewIndex.ToString();
            GameObject view = Instantiate(Resources.Load("Prefab/UI/Views/" + viewName, typeof(GameObject))) as GameObject;

            view.transform.SetParent(anchorView, false);
            view.GetComponent<BaseView>().Init();
            dicView.Add(viewIndex, view.GetComponent<BaseView>());
        }
    }
    public void SwitchView(ViewIndex newView, ViewParam viewParam = null, Action callback = null)
    {
        if (currentView != null)
        {
            currentView.HideViewAnimation(() =>
            {
                currentView.gameObject.SetActive(false);
                ShowNextView(newView, viewParam, callback);
            });
        }
        else
        {
            ShowNextView(newView, viewParam, callback);
        }
    }

    private void ShowNextView(ViewIndex newView, ViewParam viewParam = null, Action callback = null)
    {
        currentView = dicView[newView];
        currentView.gameObject.SetActive(true);
        currentView.Setup(viewParam);
        currentView.ShowViewAnimation(() =>
        {
            callback?.Invoke();
        });
    }
}