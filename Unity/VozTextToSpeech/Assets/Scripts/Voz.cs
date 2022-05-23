using System;
using UnityEngine;

namespace leandrodotta.voz
{
    public class Voz : MonoBehaviour
    {
        private const string TYPE_STRING = "java.lang.String";
        private const string TYPE_UNITY_PLAYER = "com.unity3d.player.UnityPlayer";
        private const string TYPE_VOZ = "leandrodotta.voz.VozTextToSpeech";
        
        private AndroidJavaObject voz;
        private AndroidJavaObject activityContext;

        private TextToSpeechListener listener;

        private void Start()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass activityClass = new AndroidJavaClass(TYPE_UNITY_PLAYER))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            listener = new TextToSpeechListener(this);
            voz = new AndroidJavaObject(TYPE_VOZ, activityContext, listener);
            #else
            Debug.unityLogger.LogWarning("Voz", $"Text-to-speech is only available for Android devices. Its features will not work in the current platform.{Environment.NewLine}{Environment.NewLine}Build the application in an Android device to be able to test it.");
            #endif
        }

        private void OnDestroy() 
        {
            Shutdown();    
        }

        private void Update() 
        {
            if (!enabled)
                return;

            if (listener == null)
                return;

            while (listener.ActionQueue.Count > 0)
            {
                if (listener.ActionQueue.TryDequeue(out Action action))
                {
                    action.Invoke();
                }
            }
        }

        public bool IsSpeaking
        {
            get
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return voz.Call<bool>("isSpeaking");
                #else
                Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. IsSpeaking will always return 'false'");
                return false;
                #endif
            }
        }

        public bool IsAvailable
        {
            get
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return voz.Call<bool>("isAvailable");
                #else
                Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. IsAvailable will always return 'false'");
                return false;
                #endif
            }
        }

        public float Pitch
        {
            get
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return voz.Call<float>("getPitch");
                #else
                Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. Pitch will always return '0'");
                return 0;
                #endif
            }
        }

        public float SpeechRate
        {
            get
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return voz.Call<float>("setSpeechRate");
                #else
                Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. SpeechRate will always return '0'");
                return 0;
                #endif
            }
        }

        public string CurrentVoice
        {
            get
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return voz.Call<string>("getCurrentVoice");
                #else
                Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. CurrentVoice will always return 'null'");
                return null;
                #endif
            }
        }

        public string[] Voices
        {
            get
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return voz.Call<string[]>("getVoices");
                #else
                Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'Voices' will always return 'null'");
                return null;
                #endif
            }
        }

        public void Speak(string textToSpeak, string utteranceId = null)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaObject javaTextToSpeak = new AndroidJavaObject(TYPE_STRING, textToSpeak);

            if (utteranceId == null)
            {
                voz.Call("speak", javaTextToSpeak);
            }
            else
            {
                AndroidJavaObject javaUtteranceId = new AndroidJavaObject(TYPE_STRING, utteranceId);
                voz.Call("speak", javaTextToSpeak, javaUtteranceId);
            }
            #else
            Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'Speak' method will not do anything");
            #endif
        }

        public void SpeakImmediately(string textToSpeak, string utteranceId = null)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaObject javaTextToSpeak = new AndroidJavaObject(TYPE_STRING, textToSpeak);

            if (utteranceId == null)
            {
                voz.Call("speakImmediately", javaTextToSpeak);
            }
            else
            {
                AndroidJavaObject javaUtteranceId = new AndroidJavaObject(TYPE_STRING, utteranceId);
                voz.Call("speakImmediately", javaTextToSpeak, javaUtteranceId);
            }
            #else
            Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'SpeakImmediately' method will not do anything");
            #endif
        }

        public bool SetPitch(float pitch)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            return voz.Call<bool>("setPitch", pitch);
            #else
            Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'SetPitch' method will not do anything and will always return 'false'");
            return false;
            #endif
        }

        public bool SetSpeechRate(float speechRate)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            return voz.Call<bool>("setSpeechRate", speechRate);
            #else
            Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'SetSpeechRate' method will not do anything and will always return 'false'");
            return false;
            #endif
        }

        public bool SetVoice(string voiceName)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaObject javaString = new AndroidJavaObject(TYPE_STRING, voiceName);
            return voz.Call<bool>("setVoice", javaString);
            #else
            Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'SetVoice' method will not do anything and will always return 'false'");
            return false;
            #endif
        }

        public void Shutdown()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            voz.Call("shutdown");
            #else
            Debug.unityLogger.LogWarning("Voz", "Text-to-speech is only available for Android devices. 'Shutdown' method will not do anything");
            #endif
        }
    }
}