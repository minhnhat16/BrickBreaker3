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
    public List<    RowScript> rows= new List<RowScript>();
    public int levelnum;
    public int totalScore;

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

        }
    }
    public string GetRowData()
    { 
        StringBuilder stringbuilder = new StringBuilder();
        for (int i = 0; i < rows.Count; i++)
        {
            stringbuilder.Append(rows[i].GetBlockData());
            totalScore += rows[i].totalScore;
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
    public void TotalScore()
    {
        //totalScore = 0;
        //GetRowData();
        //Debug.Log(totalScore);
    }
    [Button]
    private void CreatLevelScrtObj()
    {
        totalScore = 0;
        Level level = ScriptableObject.CreateInstance<Level>();
        level.bricks = GetRowData();
        level.winScore = totalScore;
        Debug.Log($"get total score {totalScore}");
        Debug.Log(GetRowData());
        //level.winScore = GetBlockNum() * 100;
        string path = $"Assets/Resources/Levels/level_{levelnum}.asset";
        AssetDatabase.CreateAsset(level, path);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = level;
        GetBlockNum();
    }
}
