using System.Collections;
using System.Collections.Generic;
using leandrodotta.voz;
using UnityEngine;
using UnityEngine.UI;

public class FeaturesSample : MonoBehaviour
{
    private const string STATUS_UNAVAILABLE = "Unavailable";
    private const string STATUS_INITIALIZING = "Initializing";
    private const string STATUS_OK = "OK";
    private const string STATUS_SPEAKING = "Speaking";
    private const string STATUS_SPEAK_DONE = "Done Speaking";

    [SerializeField] private Voz voz;

    [SerializeField] private Button buttonStatus;
    [SerializeField] private Text textButtonStatus;
    [SerializeField] private Button buttonSpeakNow;
    [SerializeField] private InputField inputSpeak;
    
    private void Start() 
    {
        textButtonStatus.text = STATUS_INITIALIZING;
    }

    private void OnInit()
    {
        textButtonStatus.text = STATUS_OK;
    }

    private void OnError()
    {
        textButtonStatus.text = STATUS_UNAVAILABLE;
    }

    private void OnSpeechStart()
    {
        textButtonStatus.text = STATUS_SPEAKING;
    }

    private void OnSpeechDone()
    {
        textButtonStatus.text = STATUS_SPEAK_DONE;
    }

    private void OnSpeechStop()
    {
        textButtonStatus.text = STATUS_SPEAK_DONE;
    }

    public void Speak(string text)
    {
        voz.Speak(text);
    }

    public void Speak()
    {
        Speak(inputSpeak.text);
    }

    public void SpeakNow()
    {
        voz.SpeakImmediately(inputSpeak.text);
    }
}
