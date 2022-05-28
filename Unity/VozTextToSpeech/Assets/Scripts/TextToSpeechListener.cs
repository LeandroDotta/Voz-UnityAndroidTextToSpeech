using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace leandrodotta.voz
{
    public class TextToSpeechListener : AndroidJavaProxy
    {
        private const string TYPE_TEXT_TO_SPEECH_LISTENER = "leandrodotta.voz.TextToSpeechListener";

        private Voz component;
        public ConcurrentQueue<Action> ActionQueue { get; private set; }

        public TextToSpeechListener(Voz component) : base(TYPE_TEXT_TO_SPEECH_LISTENER)
        {
            this.component = component;
            ActionQueue = new ConcurrentQueue<Action>();
        }

        void onInit()
        {
            Debug.Log("Voz Initialized");

            ActionQueue.Enqueue(() => {
                component.SendMessage("Init");
            });
        }

        void onError()
        {
            Debug.Log("Voz Initialization Error");

            ActionQueue.Enqueue(() => {
                component.SendMessage("Error");
            });
        }

        void onSpeechStart(string utteranceId)
        {
            Debug.Log($"Voz Speech Started ({utteranceId})");

            ActionQueue.Enqueue(() => {
                component.SendMessage("SpeechStart", utteranceId);
            });
        }

        void onSpeechDone(string utteranceId)
        {
            Debug.Log($"Voz Speech Done ({utteranceId})");

            ActionQueue.Enqueue(() => {
                component.SendMessage("SpeechDone", utteranceId);
            });
        }

        void onSpeechStop(string utteranceId)
        {
            Debug.Log($"Voz Speech Stopped ({utteranceId})");

            ActionQueue.Enqueue(() => {
                component.SendMessage("SpeechStop", utteranceId);
            });
        }

        void onSpeechError(string utteranceId, int errorCode)
        {
            Debug.Log($"Voz Speech Error ({utteranceId}) | code={errorCode}");

            ActionQueue.Enqueue(() => {
                component.SendMessage("SpeechError", utteranceId);
            });
        }
    }
}