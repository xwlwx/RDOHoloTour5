using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class GrammarTesting : MonoBehaviour {

    public string SRGSFileName = "TestGrammar.xml";
    public GameObject ObjectToModify;

    private GrammarRecognizer grammarRecognizer;
    private GrammarKeywordSwitcher gkws;
    private Dictionary<string,string> grammarData;
    private bool colorFlag = false;

	// Use this for initialization
	void Start () {
        if (string.IsNullOrEmpty(SRGSFileName))
        {
            Debug.LogError("Please specify an SRGS file name in GrammarManager.cs on " + name + ".");
            Debug.LogError("Please check your GameObject settings in GrammarManager.cs on " + name + ".");
            return;
        }

        // Instantiate the GrammarRecognizer, passing in the path to the SRGS file in the StreamingAssets folder.
        try
        {
            grammarRecognizer = new GrammarRecognizer(Application.streamingAssetsPath +"/"+SRGSFileName);
            grammarRecognizer.OnPhraseRecognized += GrammarRecognizer_OnPhraseRecognized;
            //grammarRecognizer.Start();
            Debug.Log("grammarrec started");
            Debug.Log(grammarRecognizer.GrammarFilePath);
        }
        catch
        {
            // If the file specified to the GrammarRecognizer doesn't exist, let the user know.
            Debug.LogError("Check the SRGS file name in the Inspector on GrammarManager.cs and that the file's in the StreamingAssets folder.");
        }

        gkws = GetComponent<GrammarKeywordSwitcher>();

    }
    
    public void GrammarRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("we're hearing things: " + args.text);
        if (colorFlag)
        {
            ObjectToModify.GetComponent<Renderer>().material.color = Color.blue;
            colorFlag = false;
        }
        else
        {
            ObjectToModify.GetComponent<Renderer>().material.color = Color.yellow;
            colorFlag = true;
        }

        grammarData = new Dictionary<string, string>();

        foreach (var i in args.semanticMeanings)
        {
            Debug.Log(i.key);
            foreach (var v in i.values)
            {
                Debug.Log(v);
            }
            grammarData.Add(i.key, i.values[0]);//IGNORES MULTIPLE VALUES
        }
        handleGrammarData();
        gkws.EnableKeywordListener();

    }

    private void handleGrammarData()
    {
        string contextValue;
        if(grammarData.TryGetValue("context", out contextValue))
        {
            switch (contextValue)
            {
                case "location":
                    handleLocationContext();
                    break;
                case "information":
                    handleInformationContext();
                    break;
                case "paint":
                    handlePaintContext();
                    break;
                default:
                    Debug.LogError("grammar context not recognized: " + contextValue);
                    break;
            }
        }
        else
        {
            Debug.LogError("grammarData doesn't contain valid context");
        }
    }

    private void handleLocationContext()
    {
        //where is ...
        string name;
        if(grammarData.TryGetValue("name", out name))
        {
            Debug.Log("Handling location context for " + name);
            //LOGAN code here
        }
        else
        {
            Debug.LogError("grammarData doesn't contain valid location context");
        }
    }

    private void handleInformationContext()
    {
        //what does...
        string name;
        if (grammarData.TryGetValue("name", out name))
        {
            Debug.Log("Handling information context for " + name);
            //LOGAN code here
        }
        else
        {
            Debug.LogError("grammarData doesn't contain valid information context");
        }
    }

    private void handlePaintContext()
    {
        //color tractor...
        string obj;
        string color;
        bool successful = true;
        if(!grammarData.TryGetValue("object", out obj))
        {
            successful = false;
        }
        if (!grammarData.TryGetValue("color", out color))
        {
            successful = false;
        }

        if (successful)
        {
            switch (obj)
            {
                case "tractor":
                    break;
                case "tires":
                    break;
            }
        }
        else
        {
            Debug.LogError("grammarData doesn't contain valid paint context");
        }
    }

    public void StartGrammarRecognizer()
    {
        Debug.Log("GrammarRecognizer starting");
        if(grammarRecognizer != null && !grammarRecognizer.IsRunning)
        {
            grammarRecognizer.Start();
        }
    }

    public void StopGrammarRecognizer()
    {
        Debug.Log("GrammarRecognizer stopping");
        if (grammarRecognizer != null && grammarRecognizer.IsRunning)
        {
            grammarRecognizer.Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
