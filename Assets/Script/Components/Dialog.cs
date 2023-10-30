using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] VoiceLine voiceLine;
    [SerializeField] AudioSource voiceSource;
    [SerializeField] bool startOnAwake;
    
    VoiceLine.Dialog[] dialogs;

    Coroutine currentEngine = null;

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
        dialogs = voiceLine.dialogs;
    }

    IEnumerator PlayLine(VoiceLine.Dialog dialog)
    {
        SubtitleManager subtitleManager = GameManager.instance.subtitleManager;
        yield return new WaitForSeconds(dialog.delayStart);
        subtitleManager.CreateSubtitle(dialog.subtitle);
        if (!voiceSource) GameManager.instance.audioManager.voice.PlayOneShot(dialog.voice);
        else voiceSource.PlayOneShot(dialog.voice);
        yield return new WaitForSeconds(dialog.voice.length);
        subtitleManager.HideSubtitle();
        yield return new WaitForSeconds(dialog.delayEnd);
    }

    IEnumerator Engine() 
    {
        Initialize();
        yield return new WaitForEndOfFrame();
        foreach (var dialog in dialogs)
        {
            yield return StartCoroutine(PlayLine(dialog));
            yield return null;
        }
        currentEngine = null;
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
        GameManager.instance.subtitleManager.HideSubtitle();
        if (!voiceSource) GameManager.instance.audioManager.voice.Stop();
        else voiceSource.Stop();
        StopAllCoroutines();
        currentEngine = null;
    }
}
