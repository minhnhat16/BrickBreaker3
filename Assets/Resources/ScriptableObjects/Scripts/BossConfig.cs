using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossConfig", menuName = "Config/BossConfig ")]
public class BossConfig : BYDataTable<BossData>
{
    public override ConfigCompare<BossData> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<BossData>("ID");
        return configCompare;
    }
}
[System.Serializable]
public class BossData
{
    [SerializeField] private int id;
    [SerializeField] private int HP;
    public int _iD { get => id; }
    public int _HP { get => HP; }
}
