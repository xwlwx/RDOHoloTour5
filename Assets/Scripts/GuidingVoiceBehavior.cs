using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class GuidingVoiceBehavior : MonoBehaviour {

    private TextToSpeech textToSpeech;

    private void Awake()
    {
        textToSpeech = GetComponent<TextToSpeech>();
    }

    public void SayText(string text)
    {
        Debug.Log("Saying: " + text);
        if(textToSpeech != null)
        {
            textToSpeech.StartSpeaking(text);
        }
    }
}
