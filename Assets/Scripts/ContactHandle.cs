using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    NONE,
    LEFT,
    RIGHT,
    TOP,
    OTHERS
}

[System.Serializable]

public class ContactHandle
{
    public Transform contactUnit;
    public UnitType unitType;
}


