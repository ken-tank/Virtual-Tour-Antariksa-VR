using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [HideInInspector] public AudioManager audioManager = null;
    public SceneSettings sceneSettings = null;
    public SubtitleManager subtitleManager;
    [HideInInspector] public FrontCanvas frontCanvas = null;

    void Awake() 
    {
        instance = this;

        if (audioManager == null)
        {
            if (FindObjectsOfType<AudioManager>().Length > 0) 
            {
                audioManager = FindObjectOfType<AudioManager>();
            }
            else
            {
                audioManager = Instantiate<AudioManager>(Resources.Load<AudioManager>("Management/Game/Audio Manager"), Vector3.zero, Quaternion.identity);
                audioManager.gameObject.name = "Audio Manager";
            }
        }

        if (frontCanvas == null)
        {
            if (FindObjectsOfType<FrontCanvas>().Length > 0) 
            {
                frontCanvas = FindObjectOfType<FrontCanvas>();
            }
            else
            {
                frontCanvas = Instantiate<FrontCanvas>(Resources.Load<FrontCanvas>("Management/UI/Front Canvas"), Vector3.zero, Quaternion.identity);
                frontCanvas.gameObject.name = "Front Canvas";
            }
        }
    }

    public void Quit() 
    {
        Application.Quit();
    }

    public void MusicVolume(bool value)
    {
        audioManager.music.volume = value ? 1 : 0;
    }

    public void EffectVolume(bool value)
    {
        audioManager.effect.volume = value ? 1 : 0;
    }

    public float TimeScale 
    {
        get {
            return Time.timeScale;
        }
        set {
            Time.timeScale = value;
        }
    }
}
