using System.Collections.Generic;
using UnityEngine;
using System.Text;
using NaughtyAttributes;

public class RowScript : MonoBehaviour
{

    public int index;
    public int totalScore = 0;
    public List<ButtonTool> blocks = new List<ButtonTool>();
    public ButtonTool prefab;
    private void Start()
    {
        AssignIndex();
    }

    public void AssignIndex()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].index = i;
        }
    }

    [Button]
    private void TestBlockData()
    {
        Debug.Log(GetBlockData());
       // Debug.Log(totalScore);
        
    }
    public string GetBlockData()
    {
        totalScore = 0;
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < blocks.Count; i++)
        {
            stringBuilder.Append(blocks[i].value);
            totalScore = CountScore(blocks[i].value);
            stringBuilder.Append(";");
        }
        return stringBuilder.ToString();
    }
    public int CountScore(int _value){

        if (_value > 0 && _value <= 10)
        {
            totalScore += 100;
        }
        else if (_value >= 11 && _value <= 22)
        {
            totalScore += 100 * 2;
        }
        else if (_value == 23)
        {
            totalScore += 100 * 3;
        }
        return totalScore;
}   
}
