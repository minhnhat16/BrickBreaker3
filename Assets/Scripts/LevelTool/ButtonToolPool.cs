using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToolPool : MonoBehaviour
{
    [System.NonSerialized]
    public BY_Local_Pool<ButtonTool> pool;
    public ButtonTool prefab;
    public static ButtonToolPool instance;
    public int spawnAmount;
    public int destroyCount;
    private void Awake()
    {
        instance = this;
        destroyCount = 0;
        pool = new BY_Local_Pool<ButtonTool>(prefab, 9, this.transform);
    }
}
