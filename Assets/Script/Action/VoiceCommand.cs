using KKSpeech;
using UnityEngine;

public class VoiceCommand : MonoBehaviour
{
    void Start()
    {
        SpeechRecognizer.StartRecording(false);
    }
}
