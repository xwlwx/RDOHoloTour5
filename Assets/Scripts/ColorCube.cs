using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ColorCube : MonoBehaviour, IInputClickHandler {

    Renderer rend;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        rend.material.color = Color.blue;
    }

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
