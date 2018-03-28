using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToObjectBehavior : MonoBehaviour {

    public GameObject ObjectToPointTo;
    public bool IsTracking { get; private set; }

    private Renderer renderer;

    public void StartTracking()
    {
        IsTracking = true;
        renderer.enabled = true;
        Debug.Log("Pointing to Object: " + ObjectToPointTo.name);
    }

    public void StopTracking()
    {
        IsTracking = false;
        renderer.enabled = false;
    }

	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<Renderer>();
        StopTracking();
    }
	
	// Update is called once per frame
	void Update () {
        if (IsTracking && ObjectToPointTo != null)
        {
            Vector3 direction = ObjectToPointTo.transform.position - this.transform.position;
            if (direction.sqrMagnitude < 1)
            {
                StopTracking();
            }
            else
            {
                this.transform.forward = direction;
            }
        }
    }


}
