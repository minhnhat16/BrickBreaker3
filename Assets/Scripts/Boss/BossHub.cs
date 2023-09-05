using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHub : MonoBehaviour
{
    public Image fg_image;
    public RectTransform anchor;
    [SerializeField] private RectTransform parent;
    [SerializeField] private Transform anchor2d;

    public void Setup(RectTransform parent, Transform anchor2d)
    {
        Debug.Log("Setup on boss hub");

        this.parent = parent;
        this.anchor2d = anchor2d;
        Debug.Log($"PARENT {parent} ANCHOR 2D {anchor2d}");
        anchor.SetParent(parent, false);
        anchor.anchoredPosition  = Vector2.zero;
    }
    public void ShowEffect(int hp, int maxhp)
    { 

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        StartCoroutine(LoadPosition());
    }
    IEnumerator LoadPosition() {
        yield return new WaitUntil( () => parent != null);
        Vector3 worldPos = anchor2d.position;
        Vector2 screenPos = CameraMain.instance.main.WorldToScreenPoint(worldPos);
        Vector2 localUIPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPos, null, out localUIPoint);
        anchor.anchoredPosition = localUIPoint;
    }
}
