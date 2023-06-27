using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenView : BaseView
{
    public void PlayBtn()
    {
        ViewManager.Instance.SwitchView(ViewIndex.SelectLevelView, null, null);
    }
}
