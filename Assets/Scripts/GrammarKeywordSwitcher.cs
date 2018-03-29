using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GrammarKeywordSwitcher : MonoBehaviour {

    public TextMesh DisplayText;
    public AudioSource HeyHoloDing;

    private GrammarParser gt;
    private SpeechInputSource sis;

    private bool grammarActive = false;

    public void EnableGrammarRecognizer()
    {
        if (!grammarActive)
        {
            Debug.Log("Enabling GrammarRecognizer");
            sis.StopKeywordRecognizer();
            gt.StartGrammarRecognizer();
            DisplayText.text = "GrammarRecognizer Active";
            grammarActive = true;
        }
    }

    public void EnableKeywordListener()
    {
        if (grammarActive)
        {
            Debug.Log("Enabling KeywordListener");
            gt.StopGrammarRecognizer();
            sis.StartKeywordRecognizer();
            DisplayText.text = "KeywordListener Active";
            grammarActive = false;
        }
    }

    public void ChangeSpeechMode()
    {
        if (grammarActive)
        {
            EnableKeywordListener();
        }
        else
        {
            EnableGrammarRecognizer();
        }
    }

    public void OnHeyHolo()
    {
        EnableGrammarRecognizer();
        HeyHoloDing.Play();
    }

    public void Start()
    {
        gt = GetComponent<GrammarParser>();
        if(gt == null)
        {
            Debug.LogError("grammartesting null");
        }
        sis = GetComponent<SpeechInputSource>();
        if(sis == null)
        {
            Debug.LogError("speechinputsource null");
        }
        //StartCoroutine(delayedInit());
    }

    private IEnumerator delayedInit()
    {
        yield return new WaitForSeconds(.1f);
        Debug.Log("Enabling KeywordListener");
        gt.StopGrammarRecognizer();
        sis.StartKeywordRecognizer();
        DisplayText.text = "KeywordListener Active";
        grammarActive = false;
    }

}
