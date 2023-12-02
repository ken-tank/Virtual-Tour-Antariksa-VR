using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Voice Lines", menuName = "Dialog/Voice Lines")]
public class VoiceLine : ScriptableObject
{
    [System.Serializable]
    public class Dialog {
        [System.Serializable]
        public class Events {
            public UnityEvent onStart, onEnd;
        }

        public float delayStart;
        public AudioClip voice;
        [TextArea] public string subtitle;
        public float delayEnd;
        public Events events;
    }

    public Dialog[] dialogs;
}
