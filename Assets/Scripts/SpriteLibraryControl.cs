using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLibraryControl : MonoBehaviour
{
    public static SpriteLibraryControl Instance;
    [SerializeField]
    private List<Sprite> spriteList = new List<Sprite>();
    [SerializeField]
    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for(int i = 0;i  < spriteList.Count; i++)
        {
            string spritename = spriteList[i].name;
            AddSprite(spritename, spriteList[i]);
            Debug.LogWarning("ADDED SPRITE TO DICT" + spritename); 
        }
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


