using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private Transform currentParent;

    public void HideParentComponent()
    {
        currentParent = this.transform.parent;
        currentParent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}
