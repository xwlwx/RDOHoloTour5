using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIConsumer : MonoBehaviour {

    public TextMesh DisplayText;
    public TextMesh ErrorDisplayText;

    public void Handle()
    {
        StartCoroutine(CheckInternet());
    }

    IEnumerator CheckInternet()
    {
        Debug.Log("attempting to connect to internet");
        /*
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("Value=hello"));
        UnityWebRequest www = UnityWebRequest.Post("http://rdoedata-dev:8033/api/values", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }*/

        
        using (WWW www = new WWW("http://rdoedata-dev:8033/api/values/MENK5610"))
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

            HoloEmployee firstemp = new HoloEmployee() {
                Box_ID = "hello",
                Parent_Box_ID = "parent",
                FirstName = "colton",
                LastName = "keller",
                Email = "meh",
                PhotoURL = "hi",
                Title = "me"
            };
            string first = JsonUtility.ToJson(firstemp);
            HoloEmployee secemp = JsonUtility.FromJson<HoloEmployee>(first);

            //string fromAPI = "{\"Box_ID\":\"MENK5610\",\"Parent_Box_ID\":\"TROS6327\",\"FirstName\":\"Chad\",\"LastName\":\"Menke\",\"Title\":\"Software Developer Supervisor\",\"PhotoURL\":\"http://myportal.rdoequipment.com/employeesearch/images/orgchartimages/MENK5610.jpg\",\"Email\":\"CMenke@rdoequipment.com\"}";

            HoloOrgChartTree myTree = JsonUtility.FromJson<HoloOrgChartTree>(www.text);
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
