using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> onMouseDown = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> onMouseStay = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> onMouseUp = new UnityEvent<Vector3>();
    public static InputManager instance;

    private void Awake()
    {
        Debug.Log("AWAKE INPUTMANAGER");
        instance = this;
    }

    void Update()
    {


    }
    public bool CheckEvent()
    {
        if (onMouseDown == null || onMouseStay == null || onMouseUp == null)
        {
            Debug.Log("CHECK ITEM EVENT FALSE");
            return false;
        }
        else 

            return true;


    }
    public void OnListened()
    {
        CheckEvent();
        if (InGameController.Instance.slider != null && CheckEvent())
        {
            if (Input.GetMouseButtonDown(0))
            {
                onMouseDown?.Invoke(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                Debug.Log("GetMouseButton");
                onMouseStay?.Invoke(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                onMouseUp?.Invoke(Input.mousePosition);
            }
        }
        else
        {
            Debug.Log("SLIDER NULL");
        }
    }

}
