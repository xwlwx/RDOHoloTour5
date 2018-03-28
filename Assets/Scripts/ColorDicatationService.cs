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
        StringColorDictionary.Add("BLUE", Color.blue);
        StringColorDictionary.Add("YELLOW", Color.yellow);
        StringColorDictionary.Add("BLACK", Color.black);
        StringColorDictionary.Add("GREY", Color.grey);
        StringColorDictionary.Add("GRAY", Color.gray);
        StringColorDictionary.Add("WHITE", Color.white);
        StringColorDictionary.Add("RED", Color.red);
        StringColorDictionary.Add("CYAN", Color.cyan);
        StringColorDictionary.Add("GREEN", Color.green);
        StringColorDictionary.Add("CLEAR", Color.clear);
        StringColorDictionary.Add("MAGENTA", Color.magenta);
    }
}
