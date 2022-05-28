using System.Collections;
using System.Collections.Generic;
using leandrodotta.voz;
using UnityEngine;
using UnityEngine.UI;

namespace leandrodotta.voz.sample
{
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

        [Header("UI")]
        [SerializeField] private Button buttonStatus;
        [SerializeField] private Text textButtonStatus;
        [SerializeField] private Image imageButtonStatus;
        [SerializeField] private Button buttonSpeakNow;
        [SerializeField] private InputField inputSpeak;
        [SerializeField] private Button buttonStop;

        [Header("Colors")]
        [SerializeField] private Color colorUnavailable = Color.gray;
        [SerializeField] private Color colorOK = Color.blue;
        [SerializeField] private Color colorSpeaking = Color.yellow;

        [Header("Components")]
        [SerializeField] private Animator animButton;

        private Voz voz;

        private void Start()
        {
            voz = GetComponent<Voz>();

            textButtonStatus.text = TEXT_INITIALIZING;
            imageButtonStatus.color = colorUnavailable;
        }

        private void Update() 
        {
            buttonStop.interactable = voz.IsSpeaking;    
        }

        private void Init()
        {
            textButtonStatus.text = TEXT_OK;
            imageButtonStatus.color = colorOK;
            animButton.SetInteger("status", STATUS_OK);
        }

        private void Error()
        {
            textButtonStatus.text = TEXT_UNAVAILABLE;
            imageButtonStatus.color = colorUnavailable;
            animButton.SetInteger("status", STATUS_UNAVAILABLE);
        }

        private void SpeechStart()
        {
            textButtonStatus.text = TEXT_SPEAKING;
            imageButtonStatus.color = colorSpeaking;
            animButton.SetInteger("status", STATUS_SPEAKING);
        }

        private void SpeechDone()
        {
            textButtonStatus.text = TEXT_SPEAK_DONE;
            imageButtonStatus.color = colorOK;
            animButton.SetInteger("status", STATUS_OK);
        }

        private void SpeechStop()
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

        public void Stop()
        {
            voz.Stop();
        }
    }
}