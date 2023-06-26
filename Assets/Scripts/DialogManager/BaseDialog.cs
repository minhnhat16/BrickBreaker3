
using System;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex dialogIndex;
    private BaseDialogAnimation baseDialogAnim;

    private void Awake()
    {
        baseDialogAnim = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }

    public void Init()
    {
        OnInit();
        gameObject.SetActive(false);
    }

    public virtual void OnInit() { }

    public virtual void Setup(DialogParam dialogParam) { }

    public void ShowDialogAnimation(Action callback)
    {
        baseDialogAnim.ShowDialogAnimation(() =>
        {
            OnStartShowDialog();
            callback?.Invoke();
            OnEndShowDialog();
        });
    }

    public void HideDialogAnimation(Action callback)
    {
        baseDialogAnim.HideDialogAnimation(() =>
        {
            OnStartHideDialog();
            callback?.Invoke();
            OnEndHideDialog();
        });
    }

    public virtual void OnStartShowDialog() { }

    public virtual void OnEndShowDialog() { }

    public virtual void OnStartHideDialog() { }

    public virtual void OnEndHideDialog() { }

    public virtual void ShowDialog() { }

    public virtual void HideDialog() { }
}
