using UnityEngine;

[CreateAssetMenu(fileName = "Voice Lines", menuName = "Dialog/Voice Lines")]
public class VoiceLine : ScriptableObject
{
    [System.Serializable]
    public class Dialog {
        public float delayStart;
        public AudioClip voice;
        [TextArea] public string subtitle;
        public float delayEnd;
    }

    public Dialog[] dialogs;
}
