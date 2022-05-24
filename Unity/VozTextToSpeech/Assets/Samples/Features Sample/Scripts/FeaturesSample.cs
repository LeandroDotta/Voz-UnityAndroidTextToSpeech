using System.Collections;
using System.Collections.Generic;
using leandrodotta.voz;
using UnityEngine;
using UnityEngine.UI;

public class FeaturesSample : MonoBehaviour
{
    private const int STATUS_UNAVAILABLE = 0;
    private const int STATUS_OK = 1;
    private const int STATUS_SPEAKING = 2;
    private const string TEXT_UNAVAILABLE = "Unavailable";
    private const string TEXT_INITIALIZING = "Initializing";
    private const string TEXT_OK = "OK";
    private const string TEXT_SPEAKING = "Speaking";
    private const string TEXT_SPEAK_DONE = "Done Speaking";


    [SerializeField] private Voz voz;

    [SerializeField] private Button buttonStatus;
    [SerializeField] private Text textButtonStatus;
    [SerializeField] private Image imageButtonStatus;
    [SerializeField] private Button buttonSpeakNow;
    [SerializeField] private InputField inputSpeak;
    [SerializeField] private Animator animButton;

    [Header("Colors")]
    [SerializeField] private Color colorUnavailable = Color.gray;
    [SerializeField] private Color colorOK = Color.blue;
    [SerializeField] private Color colorSpeaking = Color.yellow;
    
    private void Start() 
    {
        textButtonStatus.text = TEXT_INITIALIZING;
        imageButtonStatus.color = colorUnavailable;
    }

    private void OnInit()
    {
        textButtonStatus.text = TEXT_OK;
        imageButtonStatus.color = colorOK;
        animButton.SetInteger("status", STATUS_OK);
    }

    private void OnError()
    {
        textButtonStatus.text = TEXT_UNAVAILABLE;
        imageButtonStatus.color = colorUnavailable;
        animButton.SetInteger("status", STATUS_UNAVAILABLE);
    }

    private void OnSpeechStart()
    {
        textButtonStatus.text = TEXT_SPEAKING;
        imageButtonStatus.color = colorSpeaking;
        animButton.SetInteger("status", STATUS_SPEAKING);
    }

    private void OnSpeechDone()
    {
        textButtonStatus.text = TEXT_SPEAK_DONE;
        imageButtonStatus.color = colorOK;
        animButton.SetInteger("status", STATUS_OK);
    }

    private void OnSpeechStop()
    {
        textButtonStatus.text = TEXT_SPEAK_DONE;
        imageButtonStatus.color = colorOK;
        animButton.SetInteger("status", STATUS_OK);
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
