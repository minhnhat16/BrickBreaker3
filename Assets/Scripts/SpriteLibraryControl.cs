using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLibraryControl : MonoBehaviour
{
    public static SpriteLibraryControl Instance;
    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
    private void Awake()
    {
        Instance = this;
    }
    public void AddSprite(string spriteName, Sprite sprite)
    {
        if (!spriteDict.ContainsKey(spriteName))
        {
            spriteDict.Add(spriteName, sprite);
        }
        else
        {
            Debug.LogWarning("A sprite with the name " + spriteName + " already exists in the Dictionary.");
        }
    }
    public Sprite GetSpriteByName(string spriteName)
    {
        Sprite sprite = null;
        spriteDict.TryGetValue(spriteName, out sprite);
        return sprite;
    }
}
