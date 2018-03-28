using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class RecorderToggleBehavior : MonoBehaviour, HoloToolkit.Unity.InputModule.IInputClickHandler {

    public GameObject ObjectToToggle;
    private VoiceCommandManager voiceCommandManager;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        voiceCommandManager.BeginListening();
    }

    // Use this for initialization
    void Start () {
        voiceCommandManager = ObjectToToggle.GetComponent<VoiceCommandManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
