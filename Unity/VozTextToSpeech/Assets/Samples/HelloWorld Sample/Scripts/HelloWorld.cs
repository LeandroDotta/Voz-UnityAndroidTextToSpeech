using UnityEngine;
using UnityEngine.UI;

namespace leandrodotta.voz.sample
{
    public class HelloWorld : MonoBehaviour
    {
        [SerializeField] private Voz voz;

        [SerializeField] private Button buttonSpeak;

        private void Start() 
        {
            buttonSpeak.interactable = false;    
        }

        public void Speak() 
        {
            if (voz.IsAvailable)
            {
                voz.Speak("Hello World");
                buttonSpeak.interactable = false;
            }
        }

        private void OnInit()
        {
            buttonSpeak.interactable = true;
        }

        private void OnSpeechDone(string utteranceId) 
        {
            buttonSpeak.interactable = true;
        }
    }
}