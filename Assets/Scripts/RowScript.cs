using System.Collections.Generic;
using UnityEngine;
using System.Text;
using NaughtyAttributes;

public class RowScript : MonoBehaviour
{
    public int index;
    public List<ButtonTool> blocks = new List<ButtonTool>();

    private void Start()
    {
        AssignIndex();
    }

    public void AssignIndex()
    {
        for(int i = 0;i < blocks.Count; i++)
        {
            blocks[i].index = i;
        }
    }
    [Button]
    private void TestBlockData()
    {
        Debug.Log(GetBlockData());
    }
    public string GetBlockData()
    {   StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i<blocks.Count; i++)
        {
            stringBuilder.Append(blocks[i].value);
            stringBuilder.Append(";");
        }
        return stringBuilder.ToString();
    }
}
