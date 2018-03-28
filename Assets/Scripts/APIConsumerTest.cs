using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIConsumerTest : MonoBehaviour {

    public TextMesh DisplayText;
    public TextMesh ErrorDisplayText;

    public void Handle()
    {
        StartCoroutine(CheckInternet());
    }

    IEnumerator CheckInternet()
    {
        Debug.Log("attempting to connect to internet");
        using (WWW www = new WWW("http://rdoedata-dev:8033/api/values"))
        {
            yield return www;
            Debug.Log("Got www response: " + www.text);
            Debug.Log("www error: " + www.error);
            Debug.Log("www complete: " + www.isDone);
            Debug.Log("www bytes received: " + www.bytesDownloaded);
            Debug.Log("www type: " + www.GetType());
            Debug.Log("www progress: " + www.progress);
            Debug.Log("www bytes size: " + www.bytes.Length);
            string bytestring = "";
            foreach (byte b in www.bytes)
            {
                bytestring += (char)b;
            }
            Debug.Log("www bytes content: " + bytestring);
            Debug.Log("www equal \"hello api\": " + www.text.Contains("hello api"));
            DisplayText.text = www.text;
            ErrorDisplayText.text = www.error;
            foreach (string key in www.responseHeaders.Keys)
            {
                string value = "";
                www.responseHeaders.TryGetValue(key, out value);
                Debug.Log("key: " + key + "\tvalue: " + value);
            }
            if(www == null)
            {
                Debug.LogError("WWW is null");
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
