using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class CubeBehavior : MonoBehaviour, IInputClickHandler {

    public GrammarTesting gt;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.green;
        gt.GrammarRecognizer_OnPhraseRecognized(new UnityEngine.Windows.Speech.PhraseRecognizedEventArgs());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
