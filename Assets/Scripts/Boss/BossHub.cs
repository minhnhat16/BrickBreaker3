using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHub : MonoBehaviour
{
    public Image fg_image;
    public RectTransform anchor;
    public GameObject objectDisplay;
    [SerializeField] private RectTransform parent;
    [SerializeField] private Transform anchor2d;
    [SerializeField] private float timeCount;
    [SerializeField] private float value;
    [SerializeField] private float currentValue;

    public void Setup(RectTransform parent, Transform anchor2d)
    {
        Debug.Log("Setup on boss hub");
        currentValue = value = 1;   
        timeCount = 2f  ;
        this.parent = parent;
        this.anchor2d = anchor2d;
        Debug.Log($"PARENT {parent} ANCHOR 2D {anchor2d}");
        anchor.SetParent(parent, false);
        anchor.anchoredPosition  = Vector2.zero;
    }
    public void ShowEffect(int hp, int maxhp)
    {
        timeCount = 2f;
        value = (float) hp / (float)maxhp;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        objectDisplay.SetActive(timeCount  >0);
        currentValue = Mathf.Lerp(currentValue, value, Time.deltaTime * 2);
        fg_image.fillAmount = currentValue;
        
    }
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
    public void ResetHub()
    {
        Debug.LogError("RESET HUB");
        currentValue = value = 1;
        timeCount = 2f;

    }
}
