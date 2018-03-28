using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class VoiceDisplayBehavior : MonoBehaviour, HoloToolkit.Unity.InputModule.IDictationHandler {

    public TextMesh TextOutput;
    public GameObject GuidingVoice;
    public string LastVoiceCommandString { get; private set; }

    private GuidingVoiceBehavior GuidingVoiceBehavior;
    private bool isRecording = false;

    public void OnDictationComplete(DictationEventData eventData)
    {
        LastVoiceCommandString = eventData.DictationResult;
        GuidingVoiceBehavior.SayText(LastVoiceCommandString);
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.blue;
    }

    public void OnDictationError(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.red;
    }

    public void OnDictationHypothesis(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.yellow;
    }

    public void OnDictationResult(DictationEventData eventData)
    {
        LastVoiceCommandString = eventData.DictationResult;
        GuidingVoiceBehavior.SayText(LastVoiceCommandString);
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.blue;
    }

    //public void OnInputClicked(InputClickedEventData eventData)
    //{
    //    ToggleRecording();
    //}

    public void ToggleRecording()
    {
        if (isRecording)
        {
            isRecording = false;
            StartCoroutine(DictationInputManager.StopRecording());
            TextOutput.color = Color.white;
        }
        else
        {
            isRecording = true;
            StartCoroutine(DictationInputManager.StartRecording(
                this.gameObject));
            TextOutput.color = Color.green;
            TextOutput.text = "...";
        }
    }

    void Awake()
    {
        GuidingVoiceBehavior = GuidingVoice.GetComponent<GuidingVoiceBehavior>();
    }

    // Use this for initialization
    void Start () {
        TextOutput.text = "HelloText";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
