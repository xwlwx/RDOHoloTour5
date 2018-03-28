using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceResponceBehavior : MonoBehaviour {

    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}

    public void ChangeColor()
    {
        rend.material.color = Color.yellow;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
