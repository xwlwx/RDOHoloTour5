using System;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class ObjectCollectionBoard : MonoBehaviour
{

    public TextMesh MainDisplay;
    public TextMesh SubDisplay;

    public HologramManager holo;

    public bool HideText = false;

    public int activeHolograms = HologramManager.ActiveHolograms.Count;

    public string MainDisplayText
    {
        get
        {
            return "Commands: \n" + "Toggle Mesh \n" + "Toggle Board \n " + "Create Cube \n " + "Create Sphere"; ;
        }

    }

    public string SubDisplayText
    {
        get
        {
            return "Active Holograms: " + activeHolograms;
        }

    }

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    public void ToggleBoard()
    {
        HideText = !HideText;
    }



    private void UpdateBoard()
    {
        if (MainDisplay == null || HideText)
        {
            return;
        }
        MainDisplay.text = MainDisplayText;
        SubDisplay.text = "Active Holograms: " + holo.ActiveHologramSize;

        MainDisplay.color = Color.blue;
        SubDisplay.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBoard();

    }

}
