using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TractorBehavior : MonoBehaviour, IInputClickHandler {

    private Renderer rend;
    private Material tractorBodyMaterial;
    private Material tractorTireMaterial;
    private Color JDGreen;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Tractor click");
        tractorBodyMaterial.color = Color.blue;
        tractorTireMaterial.color = Color.yellow;
    }

    public void ChangeBodyColor(Color newColor)
    {
        if(newColor == Color.green)
        {
            newColor = JDGreen;
        }
        tractorBodyMaterial.color = newColor;
    }

    public void ChangeTireColor(Color newColor)
    {
        if (newColor == Color.green)
        {
            newColor = JDGreen;
        }
        tractorTireMaterial.color = newColor;
    }

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        tractorBodyMaterial = rend.materials[0];
        tractorTireMaterial = rend.materials[1];
        JDGreen = tractorBodyMaterial.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
