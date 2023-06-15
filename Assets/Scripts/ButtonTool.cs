using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ButtonTool : MonoBehaviour
{
    public int index;
    public int value = 0;
    public Dropdown dropdown;
    public List<Sprite> sprites = new List<Sprite>();

    public void Start()
    {
        dropdown = FindAnyObjectByType<Dropdown>();
    }

    public void ChangeBrickType()
    {
        switch (dropdown.value)
        {
            case 0:
                this.GetComponent<Image>().color = Color.black;
                value = 0;
                break;
            case 1: // yellow
                this.GetComponent<Image>().color = new Color32(245, 217, 32, 255);
                value = 1;
                break;
            case 2: // green
                this.GetComponent<Image>().color = new Color32(7, 214, 35, 255);
                value = 2;
                break;
            case 3: // blue
                this.GetComponent<Image>().color = new Color32(41, 130, 252, 255);
                value = 3;
                break;
            case 4: // orange
                this.GetComponent<Image>().color = new Color32(252, 125, 41, 255);
                value = 4;
                break;
            case 5: // purple
                this.GetComponent<Image>().color = new Color32(168, 23, 242, 255);
                value = 5;
                break;
            case 6: // red
                this.GetComponent<Image>().color = new Color32(255, 39, 85, 255);
                value = 6;
                break;
            case 7: // white
                this.GetComponent<Image>().color = Color.white;
                value = 7;
                break;
            case 8: // deep green
                this.GetComponent<Image>().color = new Color32(52, 152, 35, 255);
                value = 8;
                break;
            case 9: // brown
                this.GetComponent<Image>().color = new Color32(77, 48, 15, 255);
                value = 9;
                break;
            case 10: // vani
                this.GetComponent<Image>().color = new Color32(250, 221, 190, 255);
                value = 10;
                break;
            case 11: // double yello
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(245, 217, 32, 255);
                value = 11;
                break;
            case 12: // double green
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(7, 214, 35, 255);
                value = 12;
                break;
            case 13: // double blue
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(41, 130, 252, 255);
                value = 13;
                break;
            case 14: // double orange
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(252, 125, 41, 255);
                value = 14;
                break;
            case 15: // double purple
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(168, 23, 242, 255);
                value = 15;
                break;
            case 16: // double red
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(255, 39, 85, 255);
                value = 16;
                break;
            case 17: // double white
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = Color.white;
                value = 17;
                break;
            case 18: // double brown
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(77, 48, 15, 255);
                value = 18;
                break;
            case 19: // double deep green
                this.GetComponent<Image>().sprite = sprites[2];
                this.GetComponent<Image>().color = new Color32(52, 152, 35, 255);
                value = 18;
                break;
            case 20: // diamond brick
                this.GetComponent<Image>().sprite = sprites[0];
                this.GetComponent<Image>().color = Color.white;
                value = 21;
                break;
            case 21: // circle brick
                this.GetComponent<Image>().sprite = sprites[1];
                this.GetComponent<Image>().color = Color.white;
                value = 22;
                break;
            case 22: // stone brick
                this.GetComponent<Image>().sprite = sprites[3];
                this.GetComponent<Image>().color = Color.white;
                value = 23;
                break;
            case 23: // wall brick
                this.GetComponent<Image>().sprite = sprites[4];
                this.GetComponent<Image>().color = Color.white;
                value = 24;
                break;
            default:
                break;
        }
    }
}
