using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System.Linq;

public class VoiceCommandManager : MonoBehaviour, IDictationHandler {

    public TextMesh TextOutput;
    public GameObject GuidingVoice;
    public string LastVoiceCommand;
    public GameObject[] ObjectsToTrack;
    public List<string> LocationKeyWords;
    public GameObject TractorToModify;
    public List<string> ModificationKeyWords;

    private GuidingVoiceBehavior guidingVoiceBehavior;
    private PointToObjectBehavior pointToObjectBehavior;
    private TractorBehavior tractorBehavior;
    private ColorDicatationService colorDicatationService;
    private bool isRecording = false;
    //private bool hasProcessed = false;

    public void BeginListening()
    {
        StartCoroutine(DelayedRecord());
    }

    public void StopListening()
    {
        if (isRecording)
        {
            isRecording = false;
            TextOutput.color = Color.white;
            StartCoroutine(DictationInputManager.StopRecording());
        }
    }

    public IEnumerator DelayedRecord()
    {
        yield return new WaitForSeconds(.1f);

        if (!isRecording)
        {
            isRecording = true;
            TextOutput.color = Color.green;
            TextOutput.text = "...";
            StartCoroutine(DictationInputManager.StartRecording(
                this.gameObject, 5, 20, 3));
        }
    }

    public void TestLog()
    {
        Debug.Log("Testing");
    }

    public void ProcessInput()
    {
        Debug.Log("ProcessingInput: " + LastVoiceCommand);
        StopListening();
        if(guidingVoiceBehavior == null)
        {
            TextOutput.text = "Can't respond to Voice Command";
            return;
        }

        List<string> words = new List<string>(LastVoiceCommand.Split(null));

        
        //clean up words
        for (int i = 0; i < words.Count; i++)
        {
            //remove chars?
            words[i] = words[i].Replace('.', ' ');
            words[i] = words[i].ToUpper();
            words[i] = words[i].Trim();
            if(words[i].Length < 3)
            {
                Debug.Log("Removing word [" + words[i] + "]", this);
                words.Remove(words[i]);
            }
        }

        foreach(string word in words)
        {
            if (LocationKeyWords.Contains(word))
            {
                words.Remove(word);
                HandleLocationRequest(words);
                break;
            }
            else if (ModificationKeyWords.Contains(word))
            {
                words.Remove(word);
                HandleModificationRequest(words);
                break;
            }
            else
            {
                Apologize();
            }
        }
    }

    public void HandleLocationRequest(List<string> words)
    {
        Debug.Log("Handling location request", this);
        if (words.Contains("SPHERE"))
        {
            LocateObject(ObjectsToTrack.Where(go => go.name.ToUpper().Contains("SPHERE")).FirstOrDefault());
        }
        else if (words.Contains("CUBE"))
        {
            LocateObject(ObjectsToTrack.Where(go => go.name.ToUpper().Contains("CUBE")).FirstOrDefault());
        }
        else
        {
            Apologize();
        }
    }

    public void HandleModificationRequest(List<string> words)
    {
        Debug.Log("Handling modification request", this);

        bool tireFlag = false;
        bool colorFlag = false;
        Color newColor = new Color();

        foreach (string word in words)
        {
            if (!tireFlag && word.StartsWith("TIRE"))
            {
                tireFlag = true;
                //words.Remove(word);
            }
            else if(!colorFlag && colorDicatationService.getColorFromString(word, out newColor))
            {
                colorFlag = true;
                //words.Remove(word);
            }
        }

        if (colorFlag)
        {
            if (tireFlag)
            {
                tractorBehavior.ChangeTireColor(newColor);
            }
            else
            {
                tractorBehavior.ChangeBodyColor(newColor);
            }
        }
        else
        {
            Apologize();
        }
    }

    public void LocateObject(GameObject go)
    {
        Debug.Log("Locating " + go.name);
        GuidingVoice.transform.position = go.transform.position;
        guidingVoiceBehavior.SayText("Over here!");
        pointToObjectBehavior.ObjectToPointTo = go;
        pointToObjectBehavior.StartTracking();
    }

    private void Apologize()
    {
        GuidingVoice.transform.position = this.gameObject.transform.position + new Vector3(0, .6f, 0) ;
        guidingVoiceBehavior.SayText("I'm sorry. I don't understand.");
    }

    public void OnDictationComplete(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.blue;
        ProcessInput();
    }

    public void OnDictationError(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.red;
        Apologize();
    }

    public void OnDictationHypothesis(DictationEventData eventData)
    {
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.yellow;
    }

    public void OnDictationResult(DictationEventData eventData)
    {
        LastVoiceCommand = eventData.DictationResult;
        TextOutput.text = eventData.DictationResult;
        TextOutput.color = Color.white;
    }
    
    void Start () {
        guidingVoiceBehavior = GuidingVoice.GetComponent<GuidingVoiceBehavior>();
        tractorBehavior = TractorToModify.GetComponent<TractorBehavior>();
        colorDicatationService = this.gameObject.transform.parent.GetComponentInChildren<ColorDicatationService>();
        pointToObjectBehavior = this.gameObject.transform.parent.GetComponentInChildren<PointToObjectBehavior>();
        if(pointToObjectBehavior == null)
        {
            Debug.LogError("no pointToObjectBehavior found", this);
        }
        else
        {
            Debug.Log("found pointToObjectBehavior", this);
        }

        LocationKeyWords.Add("WHERE");

        ModificationKeyWords.Add("CHANGE");
        ModificationKeyWords.Add("UPDATE");
        ModificationKeyWords.Add("MODIFY");
        ModificationKeyWords.Add("SET");
        ModificationKeyWords.Add("COLOR");
    }
	
}
