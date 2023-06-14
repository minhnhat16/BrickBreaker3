using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ButtonTool : MonoBehaviour
{
    public Dropdown dropdown;
    public int index;
    public List<Sprite> sprites = new List<Sprite>();
    public int value = 7;

    public void Start()
    {
        dropdown = FindAnyObjectByType<Dropdown>();
    }

    public void ChangeBrickType()
    {
        switch (dropdown.value)
        {
            case 0: //black brick
                this.GetComponent<Image>().color = Color.black;
                value = 0;
                break;
            case 1://red brick
                this.GetComponent<Image>().color = Color.red;
                value = 1;
                break;
            case 2://green brick
                this.GetComponent<Image>().color = Color.green;
                value = 2;
                break;
            case 3://blue brick
                this.GetComponent<Image>().color = Color.blue ;
                value = 3;
                break;
            case 4://yellow brick
                this.GetComponent<Image>().color = Color.yellow;
                value = 4;
                break;
            case 5://orange brick
                this.GetComponent<Image>().color = new Color32(252, 125, 41, 255);
                value = 5;
                break;
            case 6://wall brick
                this.GetComponent<Image>().color = Color.white;
                value = 6;
                break;
            case 7: // double red
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = Color.red;
                value = 7;
                break;
            case 8: // double green
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = Color.green;
                value = 8;
                break;
            case 9: //doulbe blue
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = Color.blue;
                value = 9;
                break;
            case 10: //doulbe yellow
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = Color.yellow;
                value = 10;
                break;
            default:
                break; 
        }
    }
}
