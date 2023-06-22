using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelContain : MonoBehaviour
{
    public int count;
    public List<RowScript> rows= new List<RowScript>();
    public int levelnum;

    public void Restart()
    {
        Scene sence = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sence.name);
    }
    [Button]
    private void AssignIndex()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            rows[i].index = i;
            Debug.Log(rows[i].index = i);

        }
    }
    public string GetRowData()
    {
        StringBuilder stringbuilder = new StringBuilder();
        for (int i = 0; i < rows.Count; i++)
        {
            stringbuilder.Append(rows[i].GetBlockData());
        }
        return stringbuilder.ToString(); 
    }
    public int GetBlockNum()
    {
        for(int i = 0 ; i < rows.Count; i++) 
        {
            count += rows[i].blocks.Count;
        }
        return count;
    }

    [Button]
    private void CreatLevelScrtObj()
    {
        Level level = ScriptableObject.CreateInstance<Level>();
        level.bricks = GetRowData();
        Debug.Log(GetRowData());
        level.winScore = GetBlockNum() * 100;
        string path = $"Assets/Resources/Levels/level_{levelnum}.asset";
        

        AssetDatabase.CreateAsset(level, path);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = level;
        count = 0;
    }
}
