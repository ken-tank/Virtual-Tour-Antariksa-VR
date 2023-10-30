using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    public static AudioMixer audioMixer;
    [SerializeField] int[] _framerateList = {30, 45, 60, 90};
    public static int[] framerateList;

    void Awake()
    {
        Intialize();
    }

    public void Intialize() 
    {
        audioMixer = _audioMixer;
        framerateList = _framerateList;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Audio_Master = Audio_Master;
        Audio_Effect = Audio_Effect;
        Audio_Voice = Audio_Voice;
        Audio_Music = Audio_Music;
        Audio_UI = Audio_UI;
        Quality_Framerate = Quality_Framerate;
        Quality_VSync = Quality_VSync;
        Quality_Shadow = Quality_Shadow;
    }

    public static float Audio_Master 
    {
        get
        {
            return PlayerPrefs.GetFloat("audio_mmaster", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("audio_master", value);
            float volume = Mathf.Lerp(-80, 0, value);
            GameSettings.audioMixer.SetFloat("Master_Volume", volume);
        }
    }

    public static float Audio_Music 
    {
        get
        {
            return PlayerPrefs.GetFloat("audio_music", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("audio_music", value);
            float volume = Mathf.Lerp(-80, 0, value);
            GameSettings.audioMixer.SetFloat("Music_Volume", volume);
        }
    }

    public static float Audio_Effect 
    {
        get
        {
            return PlayerPrefs.GetFloat("audio_effect", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("audio_effect", value);
            float volume = Mathf.Lerp(-80, 0, value);
            GameSettings.audioMixer.SetFloat("Effect_Volume", volume);
        }
    }

    public static float Audio_Voice
    {
        get
        {
            return PlayerPrefs.GetFloat("audio_voice", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("audio_voice", value);
            float volume = Mathf.Lerp(-80, 0, value);
            GameSettings.audioMixer.SetFloat("Voice_Volume", volume);
        }
    }

    public static float Audio_UI 
    {
        get
        {
            return PlayerPrefs.GetFloat("audio_ui", 1);
        }
        set
        {
            PlayerPrefs.SetFloat("audio_ui", value);
            float volume = Mathf.Lerp(-80, 0, value);
            GameSettings.audioMixer.SetFloat("UI_Volume", volume);
        }
    }

    public static int Quality_Framerate
    {
        get
        {
            return PlayerPrefs.GetInt("quality_framerate", 2);
        }
        set
        {
            PlayerPrefs.SetInt("quality_framerate", value);
            Application.targetFrameRate = GameSettings.framerateList[value];
        }
    }

    public static int Quality_VSync 
    {
        get 
        {
            return PlayerPrefs.GetInt("quality_vsync", 0);
        }
        set
        {
            PlayerPrefs.SetInt("quality_vsync", value);
            QualitySettings.vSyncCount = value;
        }
    }

    public static int Quality_Shadow 
    {
        get
        {
            return PlayerPrefs.GetInt("quality_shadow", 1);
        }
        set
        {
            PlayerPrefs.SetInt("quality_shadow", value);
            QualitySettings.shadowDistance = value == 1 ? 30 : 0;
        }
    }

    public static T GetOptions<T>(Options options)
    {
        int i = 0;
        float f = 0;
        bool b = false;

        switch (options)
        {
            case Options.Audio_Master:
            f = GameSettings.Audio_Master;
            break;

            case Options.Audio_Music:
            f = GameSettings.Audio_Music;
            break;

            case Options.Audio_Effect:
            f = GameSettings.Audio_Effect;
            break;

            case Options.Audio_UI:
            f = GameSettings.Audio_UI;
            break;

            case Options.Quality_Framerate:
            i = GameSettings.Quality_Framerate;
            break;

            case Options.Quality_VSyn:
            i = GameSettings.Quality_VSync;
            break;

            case Options.Quality_Shadow:
            i = GameSettings.Quality_Shadow;
            break;
        }

        if (typeof(T) == typeof(int))
        {
            return (T)(object) i;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object) f;
        }
        if (typeof(T) == typeof(bool))
        {
            return (T)(object) b;
        }
        return default(T);
    }

    public static void SetOptions<T>(Options options, T value)
    {
        switch (options)
        {
            case Options.Audio_Master:
            GameSettings.Audio_Master = (float)(object) value;
            break;

            case Options.Audio_Music:
            GameSettings.Audio_Music = (float)(object) value;
            break;

            case Options.Audio_Effect:
            GameSettings.Audio_Effect = (float)(object) value;
            break;

            case Options.Audio_UI:
            GameSettings.Audio_UI = (float)(object) value;
            break;

            case Options.Quality_Framerate:
            GameSettings.Quality_Framerate = (int)(object) value;
            break;

            case Options.Quality_VSyn:
            GameSettings.Quality_VSync = (int)(object) value;
            break;

            case Options.Quality_Shadow:
            GameSettings.Quality_Shadow = (int)(object) value;
            break;
        }
    }
}

public enum Options 
{
    None,
    Audio_Master,
    Audio_Music,
    Audio_Effect,
    Audio_UI,
    Quality_Framerate,
    Quality_VSyn,
    Quality_Shadow
}
