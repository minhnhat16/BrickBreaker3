using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[System.Serializable]
public class LevelConfigRecord
{
    [SerializeField]
    private int ID;
    [SerializeField]
    private int IsBoss;
    [SerializeField]
    private int IsWon;

    public int iD { get => ID; }
    public int isBoss { get => IsBoss; }
    public int isWon { get => IsWon; }
}

public class LevelConfig : BYDataTable<LevelConfigRecord>
{
    public override ConfigCompare<LevelConfigRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<LevelConfigRecord>("ID");
        return configCompare;
    }
}
