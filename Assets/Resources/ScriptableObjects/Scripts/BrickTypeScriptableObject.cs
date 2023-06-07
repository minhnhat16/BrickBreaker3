using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrickTypeScriptableObject", menuName = "ScriptableObjects/BrickTypeScriptableObject ")]
public class BrickTypeScriptableObject : ScriptableObject
{
    public Brick.BrickType BrickType;
    public string typeName;
    public Sprite brickSprite;
}
