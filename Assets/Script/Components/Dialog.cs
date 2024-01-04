using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField] VoiceLine.Dialog[] voiceLine;
    [HideInInspector] public AudioSource voiceSource;
    [SerializeField] bool startOnAwake;
    
    VoiceLine.Dialog[] dialogs;

    Coroutine currentEngine = null;
    SubtitleManager subtitleManager;

    public UnityEvent onStart, onEnd;

    void Start()
    {
        if (startOnAwake && currentEngine == null) StartDialog();
    }

    void OnEnable()
    {
        if (startOnAwake && currentEngine == null) StartDialog();
    }

    void OnDisable()
    {
        StopDialog();
    }

    public void Initialize() 
    {
        dialogs = voiceLine;
    }

    IEnumerator PlayLine(VoiceLine.Dialog dialog)
    {
        subtitleManager = FindObjectOfType<SubtitleManager>();
        yield return new WaitForSeconds(dialog.delayStart);
        dialog.events.onStart.Invoke();
        subtitleManager.CreateSubtitle(dialog.subtitle);
        if (!voiceSource) 
        {
            if (dialog.voice) GameManager.instance.audioManager.voice.PlayOneShot(dialog.voice);
        }
        else
        { 
            if (dialog.voice) voiceSource.PlayOneShot(dialog.voice);
        }
        yield return new WaitForSeconds(dialog.voice ? dialog.voice.length : 1);
        subtitleManager.HideSubtitle();
        yield return new WaitForSeconds(dialog.delayEnd);
        dialog.events.onEnd.Invoke();
    }

    IEnumerator Engine() 
    {
        Initialize();
        yield return new WaitForEndOfFrame();
        onStart.Invoke();
        foreach (var dialog in dialogs)
        {
            yield return StartCoroutine(PlayLine(dialog));
            yield return null;
        }
        currentEngine = null;
        onEnd.Invoke();
    }

    public void StartDialog()
    {
        if (currentEngine != null) 
        {
            StopAllCoroutines();
        }
        currentEngine = StartCoroutine(Engine());
    }

    public void StopDialog() 
    {
        if (subtitleManager) subtitleManager.HideSubtitle();
        if (!voiceSource) GameManager.instance.audioManager.voice.Stop();
        else voiceSource.Stop();
        StopAllCoroutines();
        currentEngine = null;
    }
}
