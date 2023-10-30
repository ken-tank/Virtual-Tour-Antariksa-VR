using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource effect, voice, music, ui;

    void Start() 
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip, bool loop = false) 
    {
        if (music.isPlaying && music.clip == clip)
        {
            // Pass
        }
        else
        {
            if (music.isPlaying) music.Stop();
            music.loop = loop;
            music.clip = clip;
            music.Play();
        }
    }

    public void PlayUI(AudioClip clip, float volume = 1) 
    {
        ui.PlayOneShot(clip, volume);
    }
}