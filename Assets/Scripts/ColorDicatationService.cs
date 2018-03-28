using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ColorDicatationService : MonoBehaviour {
    
    private Dictionary<string, Color> StringColorDictionary;

	public bool getColorFromString(string colorString, out Color color)
    {
        if(StringColorDictionary.TryGetValue(colorString, out color))
        {
            Debug.Log("Matched color: [" + colorString + "]");
            return true;
        }
        else
        {
            Debug.Log("Failed to match color: [" + colorString + "]");
            return false;
        }
    }

    public void Start()
    {
        if(StringColorDictionary == null)
        {
            StringColorDictionary = new Dictionary<string, Color>();
        }
        StringColorDictionary.Add("blue", Color.blue);
        StringColorDictionary.Add("yellow", Color.yellow);
        StringColorDictionary.Add("black", Color.black);
        StringColorDictionary.Add("grey", Color.grey);
        StringColorDictionary.Add("gray", Color.gray);
        StringColorDictionary.Add("white", Color.white);
        StringColorDictionary.Add("red", Color.red);
        StringColorDictionary.Add("cyan", Color.cyan);
        StringColorDictionary.Add("green", Color.green);
        StringColorDictionary.Add("clear", Color.clear);
        StringColorDictionary.Add("magenta", Color.magenta);
    }
}
