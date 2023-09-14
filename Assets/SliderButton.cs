using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SliderButton : MonoBehaviour
{
    public float speed;
    [SerializeField] private float maxX;
    public static SliderButton instance;

    private void Awake()
    {
        Debug.Log("AWAKE SLIDERBUTTON");
        instance = this;

        StartCoroutine(InitInputManager());
    }
    private IEnumerator InitInputManager()
    {
        Debug.Log("START IEnumerator");
        while (GameManager.Instance.gameObject.activeSelf == false)
        {
            Debug.Log("NULL IEnumerator");

            yield return null;
        }
        if (GameManager.Instance.InputManager.onMouseStay != null)
        {
            GameManager.Instance.InputManager.onMouseStay.AddListener(OnMouseStay);
        }
        else
        {
            Debug.Log("NON LISTIONER");
        }

    }

    private void Update()
    {
        GameManager.Instance.InputManager.OnListened();
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
        transform.position = pos;
    }

    public void OnMouseStay(Vector3 pos)
    {
        Debug.Log("ON MOUSE STAY");
        Debug.Log(Input.mousePosition);
        pos = CameraMain.instance.main.ScreenToWorldPoint(Input.mousePosition);
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
