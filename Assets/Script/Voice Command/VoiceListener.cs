using System.Collections;
using KKSpeech;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VoiceListener : MonoBehaviour
{
    [System.Serializable] 
    class UIInfo {
        [SerializeField] GameObject root;
        [SerializeField] Slider progress;
        [SerializeField] TextMeshProUGUI text;

        public void Enable(bool value) {
            root.SetActive(value);
        }

        public void SetTitle(string value) {
            text.text = value; 
        }

        public void Loading(float value) {
            progress.value = Mathf.Clamp01(value);
        }
    }

    [SerializeField] float startDelay = 2.5f;
    [SerializeField] UIInfo uIInfo;

    public UnityEvent onStart, onEnd;

    Coroutine coroutine = null;
    bool enableListener = false;

    public void SetListenerEnable(bool value)
    {
        enableListener = value;
    }

    public void StartCommand() 
    {
        if (!SpeechRecognizer.IsRecording() && enableListener)
        {
            coroutine ??= StartCoroutine(StartingListener());
        }
    }

    public void CancleCommand()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            Debug.Log("Cancle Listening");
            if (!SpeechRecognizer.IsRecording()) uIInfo.Enable(false);
            SpeechRecognizer.StopIfRecording();
        }
    }

    IEnumerator StartingListener() 
    {
        onStart.Invoke();
        Debug.Log("StaringListener...");
        uIInfo.Loading(0);
        uIInfo.SetTitle("Memulai Perintah...");
        uIInfo.Enable(true);
        float time = 0;
        while (time <= startDelay)
        {
            time += Time.unscaledDeltaTime;
            float t = time/startDelay;
            uIInfo.Loading(t);
            yield return null;
        }
        uIInfo.Loading(1);
        GameSettings.audioMixer.SetFloat("Voice_Volume", -80);
        SpeechRecognizer.StartRecording(true);
        Debug.Log("Listening...");
        uIInfo.SetTitle("Mendengarkan...");
        coroutine = null;
        onEnd.Invoke();
    }

    public void ResetUI()
    {
        uIInfo.Enable(false);
        uIInfo.Loading(0);
        uIInfo.SetTitle("");
        GameSettings.audioMixer.SetFloat("Voice_Volume", 0);
    }

    public void ResetOnStart() 
    {
        onStart = new UnityEvent();
    }

    public void ResetOnEnd() 
    {
        onEnd = new UnityEvent();
    }
}
